using GetAroundAuckland.Windows10.Interfaces;
using GetAroundAuckland.Windows10.Models;
using GetAroundAuckland.Windows10.Views;
using Prism.Commands;
using Prism.Windows.Mvvm;
using Services.NavigationService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.UI.Xaml.Navigation;

namespace GetAroundAuckland.Windows10.ViewModels
{
    public class MainPageViewModel : BaseViewModel, IMainPageViewModel
    {
        private bool _isLoading;
        private ObservableCollection<Agency> _agencies;
        private Agency _selectedAgency;
        private ObservableCollection<RouteType> _routeTypes;
        private RouteType _selectedRouteType;
        private ObservableCollection<Route> _routes;
        private ObservableCollection<Stop> _stops;

        private IDisposable _statusChanged;
        private IDisposable _positionChanged;
        private double _lastPositionLatitude;
        private double _lastPositionLongitude;
        private bool _gpsState;
        private Geolocator _geolocator;
        private Geopoint _center;
        private double _zoomLevel;

        public bool IsLoading
        {
            get {  return _isLoading; }
            private set
            {
                _isLoading = value;
                OnPropertyChanged("IsLoading");
            }
        }
        public ObservableCollection<Agency> Agencies
        {
            get { return _agencies; }
            private set
            {
                _agencies = value;
                OnPropertyChanged("Agencies");
            }
        }

        public Agency SelectedAgency
        {
            get { return _selectedAgency; }
            set
            {
                _selectedAgency = value;
                OnPropertyChanged("SelectedAgency");
            }
        }

        public ObservableCollection<RouteType> RouteTypes
        {
            get { return _routeTypes; }
            private set
            {
                _routeTypes = value;
                OnPropertyChanged("RouteTypes");
            }
        }

        public RouteType SelectedRouteType
        {
            get { return _selectedRouteType; }
            set
            {
                _selectedRouteType = value;
                OnPropertyChanged("SelectedRouteType");
            }
        }

        public ObservableCollection<Route> Routes
        {
            get { return _routes; }
            private set
            {
                _routes = value;
                OnPropertyChanged("Routes");
            }
        }

        public ObservableCollection<Stop> Stops
        {
            get { return _stops; }
            private set
            {
                _stops = value;
                OnPropertyChanged("Stops");
            }
        }

        public IDisposable StatusChanged
        {
            get { return _statusChanged; }
            private set
            {
                _statusChanged = value;
                OnPropertyChanged("StatusChanged");
            }
        }

        public IDisposable PositionChanged
        {
            get { return _positionChanged; }
            private set
            {
                _positionChanged = value;
                OnPropertyChanged("PositionChanged");
            }
        }

        public bool GpsState
        {
            get { return _gpsState; }
            set
            {
                _gpsState = value;
                OnPropertyChanged("GpsState");
            }
        }

        public double LastPositionLatitude
        {
            get { return _lastPositionLatitude; }
            set
            {
                _lastPositionLatitude = value;
                OnPropertyChanged("LastPositionLatitude");
            }
        }

        public double LastPositionLongitude
        {
            get { return _lastPositionLongitude; }
            set
            {
                _lastPositionLongitude = value;
                OnPropertyChanged("LastPositionLongitude");
            }
        }

        public Geolocator Geolocator
        {
            get { return _geolocator; }
            private set
            {
                _geolocator = value;
                OnPropertyChanged("Geolocator");
            }
        }

        public Geopoint Center
        {
            get { return _center; }
            set
            {
                _center = value;
                OnPropertyChanged("Center");
            }
        }

        public double ZoomLevel
        {
            get { return _zoomLevel; }
            set
            {
                _zoomLevel = value;
                OnPropertyChanged("ZoomLevel");
            }
        }

        public DelegateCommand<Route> TapRouteCommand { get; set; }
        public DelegateCommand<Stop> TapStopCommand { get; set; }
        public DelegateCommand TapSettingsCommand { get; set; }

        public MainPageViewModel()
        {
            TapRouteCommand = new DelegateCommand<Route>(ExecuteTapRouteCommand, CanExecuteTapRouteCommand);
            TapStopCommand = new DelegateCommand<Stop>(ExecuteTapStopCommand, CanExecuteTapStopCommand);
            TapSettingsCommand = new DelegateCommand(ExecuteTapSettingsCommand, CanExecuteTapSettingsCommand);
        }

        public override async void OnNavigatedTo(Prism.Windows.Navigation.NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            if (e.NavigationMode == NavigationMode.Back)
                return;

            try
            {
                SetGeolocator();

                IsLoading = true;

                var agencies = await SqlService.GetAgencies();
                Agencies = new ObservableCollection<Agency>(agencies.OrderBy(x => x.Name));
                Agencies.Insert(0, Agency.CreateDefault());
                SelectedAgency = Agencies.FirstOrDefault();
                RouteTypes = new ObservableCollection<RouteType>(RouteType.GetRouteTypes());
                SelectedRouteType = RouteTypes.First(x => x.Name == "bus");
                var routes = await SqlService.GetRoutes();
                Routes = new ObservableCollection<Route>(routes.OrderBy(x => x.ShortName).ThenBy(x => x.AgencyId));
                var stops = await SqlService.GetStops();
                Stops = new ObservableCollection<Stop>(stops.OrderBy(x => x.Id));
                Stop.ListOfStops = stops.ToList();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                IsLoading = false;
            }

        }

        private void SetGeolocator()
        {
            var lastLatitude = AppDataService.GetSettingsKeyValue<double>("LastLatitude");
            var lastLongitude = AppDataService.GetSettingsKeyValue<double>("LastLongitude");

            if (SettingsPageViewModel.GetGps())
            {
                if (Geolocator == null)
                {
                    Geolocator = new Geolocator();
                    Geolocator.DesiredAccuracy = PositionAccuracy.High;
                    Geolocator.MovementThreshold = 20;

                    Observable.FromEventPattern<StatusChangedEventArgs>(Geolocator, "StatusChanged")
                        .ObserveOnDispatcher()
                        .Do(x => UpdateStatusChanged(x)).Subscribe();
                    Observable.FromEventPattern<PositionChangedEventArgs>(Geolocator, "PositionChanged")
                        .ObserveOnDispatcher()
                        .Do(x => UpdatePositionChanged(x)).Subscribe();
                }



                if (lastLatitude == 0.0 && lastLongitude == 0.0)
                    ZoomLevel = 2;
                else
                {
                    ZoomLevel = 16;
                    LastPositionLatitude = lastLatitude;
                    LastPositionLongitude = lastLongitude;
                    var position = new BasicGeoposition();
                    position.Latitude = LastPositionLatitude;
                    position.Longitude = LastPositionLongitude;
                    Center = new Geopoint(position);

                    MessengerService.Send(Center, "PositionChanged");
                }
            }
            else
            {
                Geolocator = null;
                GpsState = false;
                MessengerService.Send(Center, "PositionChanged");
            }
        }

        private void UpdateStatusChanged(EventPattern<StatusChangedEventArgs> e)
        {
            if (e.EventArgs.Status == PositionStatus.Ready)
                GpsState = true;
            else
                GpsState = false;

            MessengerService.Send(Center, "PositionChanged");
        }

        private void UpdatePositionChanged(EventPattern<PositionChangedEventArgs> e)
        {
            LastPositionLatitude = e.EventArgs.Position.Coordinate.Point.Position.Latitude;
            LastPositionLongitude = e.EventArgs.Position.Coordinate.Point.Position.Longitude;
            System.Diagnostics.Debug.WriteLine(string.Format("Latitude: {0}, Longitude: {1}",
                LastPositionLatitude, LastPositionLongitude));

            var position = new BasicGeoposition();
            position.Latitude = LastPositionLatitude;
            position.Longitude = LastPositionLongitude;
            Center = new Geopoint(position);
            ZoomLevel = 16;

            AppDataService.UpdateSettingsKeyValue("LastLatitude", position.Latitude);
            AppDataService.UpdateSettingsKeyValue("LastLongitude", position.Longitude);

            //NearbySchools = new List<Directory>(SchoolsWithinsSquare(Center));
            MessengerService.Send(Center, "PositionChanged");
            //MessengerService.Send<IEnumerable<Directory>>(NearbySchools, "NearbySchoolsChanged");
        }

        public List<Route> FilterRoutes(string routeNo)
        {            
            return Routes.Where(x => x.ShortName.ToUpper().Contains(routeNo.ToUpper())).ToList();           
        }

        public List<Stop> FilterStops(string stopNo)
        {
            return Stops.Where(x => x.Id.Contains(stopNo)).ToList();
        }

        private void ExecuteTapRouteCommand(Route route)
        {
            NavigationService.Navigate(App.Experiences.Route.ToString(), route.Id);
        }

        private bool CanExecuteTapRouteCommand(Route route)
        {
            return true;
        }

        private void ExecuteTapStopCommand(Stop stop)
        {
            NavigationService.Navigate(App.Experiences.Stop.ToString(), stop.Id);
        }

        private bool CanExecuteTapStopCommand(Stop stop)
        {
            return true;
        }

        private void ExecuteTapSettingsCommand()
        {
            NavigationService.Navigate(App.Experiences.Settings.ToString());
        }

        private bool CanExecuteTapSettingsCommand()
        {
            return true;
        }
    }
}
