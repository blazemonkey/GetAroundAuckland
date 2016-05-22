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

                if (MessengerService != null)
                    MessengerService.Send(_isShowStops, "ShowStops");
            }
        }

        public DelegateCommand SelectedTripChangedCommand { get; set; }
        public DelegateCommand IsShowStopsCheckedCommand { get; set; }
        public DelegateCommand TapCenterMapCommand { get; set; }
        public DelegateCommand<StopTime> TapStopTimeCommand { get; set; }

        public RoutePageViewModel()
        {
            SelectedTripChangedCommand = new DelegateCommand(ExecuteSelectedTripChangedCommand, CanExecuteSelectedTripChangedCommand);
            IsShowStopsCheckedCommand = new DelegateCommand(ExecuteIsShowStopsCheckedCommand, CanExecuteIsShowStopsCheckedCommand);
            TapCenterMapCommand = new DelegateCommand(ExecuteTapCenterMapCommand, CanExecuteTapCenterMapCommand);
            TapStopTimeCommand = new DelegateCommand<StopTime>(ExecuteTapStopTimeCommand, CanExecuteTapStopTimeCommand);
            IsShowStops = true;
        }

        public override async void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            IsLoading = true;
            try
            {
                var routeId = e.Parameter.ToString();
                var route = await SqlService.GetRouteById(routeId);
                SelectedRoute = route;
                var agencies = await SqlService.GetAgencies();
                AgencyName = agencies.First(x => x.Id == SelectedRoute.AgencyId).Name;

                await SetTrip(SelectedRoute.Id);
                if (!Trips.Any())
                    return;

                SelectedTrip = Trips.First();

                await SetStops(SelectedTrip.Id);
                await SetCalendars(SelectedTrip.ServiceId);
                await SetShapes(SelectedTrip.ShapeId);
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
            await SetStops(SelectedTrip.Id);
            await SetCalendars(SelectedTrip.ServiceId);
            await SetShapes(SelectedTrip.ShapeId);
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
                MessengerService.Send(true, "ReCenterMapRoute");
        }

        private bool CanExecuteIsShowStopsCheckedCommand()
        {
            return true;
        }

        private void ExecuteTapCenterMapCommand()
        {
            MessengerService.Send(true, "ReCenterMapRoute");
        }

        private bool CanExecuteTapCenterMapCommand()
        {
            return true;
        }

        private void ExecuteTapStopTimeCommand(StopTime stopTime)
        {
            NavigationService.Navigate(App.Experiences.Stop.ToString(), stopTime.StopId);
        }

        private bool CanExecuteTapStopTimeCommand(StopTime stopTime)
        {
            return true;
        }

        private async Task SetTrip(string routeId)
        {
            var trips = await SqlService.GetTripsByRouteId(SelectedRoute.Id);
            if (!trips.Any())
            {
                trips = await RestService.GetTripsByRouteId(SelectedRoute.Id);
                await SqlService.AddTrips(trips);
            }

            Trips = new ObservableCollection<Trip>(trips.OrderBy(x => x.FirstArrivalTime));
        }

        private async Task SetStops(string tripId)
        {
            var stopTimes = await SqlService.GetStopTimesByTripId(tripId);
            if (!stopTimes.Any())
            {
                stopTimes = await RestService.GetStopTimesByTripId(tripId);
                await SqlService.AddStopTimes(stopTimes);
            }

            StopTimes = new ObservableCollection<StopTime>(stopTimes);
            var stops = await SqlService.GetStops();

            var stopsJoined = from stop in stops
                              join time in stopTimes
                              on stop.Id equals time.StopId
                              orderby time.StopSequence
                              select stop;
            Stops = new ObservableCollection<Stop>(stopsJoined);
        }

        private async Task SetCalendars(string serviceId)
        {
            var calendars = await SqlService.GetCalendarsByServiceId(serviceId);
            if (!calendars.Any())
            {
                calendars = await RestService.GetCalendarsByServiceId(serviceId);
                await SqlService.AddCalendars(calendars);
            }
            
            Calendars = new ObservableCollection<Models.Calendar>(calendars);

            if (!Calendars.Any())
                return;

            SelectedCalendar = Calendars.First();

            var calendarDates = await SqlService.GetCalendarDatesByServiceId(serviceId);
            CalendarDates = new ObservableCollection<CalendarDate>(calendarDates);
        }

        private async Task SetShapes(string shapeId)
        {
            var shapes = await SqlService.GetShapesById(shapeId);
            if (!shapes.Any())
            {
                shapes = await RestService.GetShapesById(shapeId);
                await SqlService.AddShapes(shapes);
            }
            
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
