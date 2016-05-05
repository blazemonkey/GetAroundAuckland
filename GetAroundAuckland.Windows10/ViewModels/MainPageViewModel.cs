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
using System.Text;
using System.Threading.Tasks;
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

        public DelegateCommand<Route> TapRouteCommand { get; set; }

        public MainPageViewModel()
        {
            TapRouteCommand = new DelegateCommand<Route>(ExecuteTapRouteCommand, CanExecuteTapRouteCommand);
        }

        public override async void OnNavigatedTo(Prism.Windows.Navigation.NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            try
            {
                IsLoading = true;
                var response = await RestService.GetApi<List<Agency>>("http://localhost:2412/api/", "agencies");
                Agencies = new ObservableCollection<Agency>(response.OrderBy(x => x.Name));
                Agencies.Insert(0, Agency.CreateDefault());
                SelectedAgency = Agencies.FirstOrDefault();
                RouteTypes = new ObservableCollection<RouteType>(RouteType.GetRouteTypes());
                SelectedRouteType = RouteTypes.First(x => x.Name == "bus");
                var routes = await RestService.GetApi<List<Route>>("http://localhost:2412/api/", "routes");
                Routes = new ObservableCollection<Route>(routes.OrderBy(x => x.ShortName).ThenBy(x => x.AgencyId));
                var stops = await RestService.GetApi<List<Stop>>("http://localhost:2412/api/", "stops");
                Stops = new ObservableCollection<Stop>(stops.OrderBy(x => x.Id));
                Stop.ListOfStops = stops.ToList();
            }
            catch (Exception)
            {

            }
            finally
            {
                IsLoading = false;
            }

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
            NavigationParameters.Instance.SetParameters(route);
            NavigationService.Navigate(App.Experiences.Route.ToString());
        }

        private bool CanExecuteTapRouteCommand(Route route)
        {
            return true;
        }
    }
}
