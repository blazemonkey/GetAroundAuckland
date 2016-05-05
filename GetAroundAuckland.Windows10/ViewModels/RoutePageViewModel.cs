using GetAroundAuckland.Windows10.Helpers;
using GetAroundAuckland.Windows10.Interfaces;
using GetAroundAuckland.Windows10.Models;
using Prism.Commands;
using Prism.Windows.Navigation;
using Services.NavigationService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace GetAroundAuckland.Windows10.ViewModels
{
    public class RoutePageViewModel : BaseViewModel, IRoutePageViewModel
    {
        private bool _isLoading;
        private Route _selectedRoute;
        private string _agencyName;
        private ObservableCollection<Trip> _trips;
        private Trip _selectedTrip;
        private ObservableCollection<StopTime> _stopTimes;
        private ObservableCollection<Stop> _stops;
        private ObservableCollection<Shape> _shapes;
        private ObservableCollection<Models.Calendar> _calendars;
        private Models.Calendar _selectedCalendar;
        private ObservableCollection<CalendarDate> _calendarDates;
        private bool _isShowStops;

        public bool IsLoading
        {
            get { return _isLoading; }
            private set
            {
                _isLoading = value;
                OnPropertyChanged("IsLoading");
            }
        }

        public Route SelectedRoute
        {
            get { return _selectedRoute; }
            private set
            {
                _selectedRoute = value;
                OnPropertyChanged("SelectedRoute");
            }
        }

        public string AgencyName
        {
            get { return _agencyName; }
            private set
            {
                _agencyName = value;
                OnPropertyChanged("AgencyName");
            }
        }

        public ObservableCollection<Trip> Trips
        {
            get { return _trips; }
            private set
            {
                _trips = value;
                OnPropertyChanged("Trips");
            }
        }

        public Trip SelectedTrip
        {
            get { return _selectedTrip; }
            set
            {
                _selectedTrip = value;
                OnPropertyChanged("SelectedTrip");
            }
        }

        public ObservableCollection<StopTime> StopTimes
        {
            get { return _stopTimes; }
            private set
            {
                _stopTimes = value;
                OnPropertyChanged("StopTimes");
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

        public ObservableCollection<Shape> Shapes
        {
            get { return _shapes; }
            private set
            {
                _shapes = value;
                OnPropertyChanged("Shapes");
            }
        }

        public ObservableCollection<Models.Calendar> Calendars
        {
            get { return _calendars; }
            private set
            {
                _calendars = value;
                OnPropertyChanged("Calendars");
            }
        }

        public Models.Calendar SelectedCalendar
        {
            get { return _selectedCalendar; }
            private set
            {
                _selectedCalendar = value;
                OnPropertyChanged("SelectedCalendar");
            }
        }

        public ObservableCollection<CalendarDate> CalendarDates
        {
            get { return _calendarDates; }
            private set
            {
                _calendarDates = value;
                OnPropertyChanged("CalendarDates");
            }
        }

        public bool IsShowStops
        {
            get { return _isShowStops; }
            set
            {
                _isShowStops = value;
                OnPropertyChanged("IsShowStops");
            }
        }

        public DelegateCommand SelectedTripChangedCommand { get; set; }
        public DelegateCommand IsShowStopsCheckedCommand { get; set; }

        public RoutePageViewModel()
        {
            SelectedTripChangedCommand = new DelegateCommand(ExecuteSelectedTripChangedCommand, CanExecuteSelectedTripChangedCommand);
            IsShowStopsCheckedCommand = new DelegateCommand(ExecuteIsShowStopsCheckedCommand, CanExecuteIsShowStopsCheckedCommand);
        }

        public override async void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            IsLoading = true;
            try
            {
                var route = (Route)NavigationParameters.Instance.GetParameters();
                SelectedRoute = route;
                var agencies = await RestService.GetApi<List<Agency>>("http://localhost:2412/api/", "agencies");
                AgencyName = agencies.First(x => x.Id == SelectedRoute.AgencyId).Name;

                var trips = await RestService.GetApi<List<Trip>>("http://localhost:2412/api/", string.Format("trips/{0}", SelectedRoute.Id));
                Trips = new ObservableCollection<Trip>(trips.OrderBy(x => x.FirstArrivalTime));

                if (!Trips.Any())
                    return;

                SelectedTrip = Trips.First();

                await SetStops(SelectedTrip);
                await SetCalendars(SelectedTrip);
                await SetShapes(SelectedTrip);
            }
            catch (Exception)
            {
                
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async void ExecuteSelectedTripChangedCommand()
        {
            await SetStops(SelectedTrip);
            await SetCalendars(SelectedTrip);
            await SetShapes(SelectedTrip);
        }

        private bool CanExecuteSelectedTripChangedCommand()
        {
            return true;
        }

        private void ExecuteIsShowStopsCheckedCommand()
        {
            if (IsShowStops)
                MessengerService.Send(Shapes, "DrawShapes");
            else
                MessengerService.Send(true, "ClearShapes");
        }

        private bool CanExecuteIsShowStopsCheckedCommand()
        {
            return true;
        }

        private async Task SetStops(Trip trip)
        {
            var stopTimes = await RestService.GetApi<List<StopTime>>("http://localhost:2412/api/", string.Format("stopTimes/{0}", trip.Id));
            StopTimes = new ObservableCollection<StopTime>(stopTimes);
            var stops = from stop in Stop.ListOfStops
                       join time in stopTimes
                       on stop.Id equals time.StopId
                       orderby time.StopSequence
                       select stop;
            Stops = new ObservableCollection<Stop>(stops);
        }

        private async Task SetCalendars(Trip trip)
        {
            var calendars = await RestService.GetApi<List<Models.Calendar>>("http://localhost:2412/api/", string.Format("calendars/{0}", trip.ServiceId));
            Calendars = new ObservableCollection<Models.Calendar>(calendars);

            if (!Calendars.Any())
                return;

            SelectedCalendar = Calendars.First();

            var calendarDates = await RestService.GetApi<List<CalendarDate>>("http://localhost:2412/api/", string.Format("calendarDates/{0}", trip.ServiceId));
            CalendarDates = new ObservableCollection<CalendarDate>(calendarDates);
        }

        private async Task SetShapes(Trip trip)
        {
            var shapes = await RestService.GetApi<List<Shape>>("http://localhost:2412/api/", string.Format("shapes/{0}", trip.ShapeId));
            Shapes = new ObservableCollection<Shape>(shapes);
        }

        public ObservableCollection<Shape> GetShapes()
        {
            return Shapes;
        }

        public ObservableCollection<Stop> GetStops()
        {
            return Stops;
        }
    }
}
