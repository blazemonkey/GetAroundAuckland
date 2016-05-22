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
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;

namespace GetAroundAuckland.Windows10.ViewModels
{
    public class StopPageViewModel : BaseViewModel, IStopPageViewModel
    {
        private bool _isLoading;
        private Stop _selectedStop;
        private bool _isLoadingMovements;
        private bool _hasMovements;
        private string _movementMessage;
        private DateTime _refreshTime;
        private ObservableCollection<Movement> _movements;
        private ObservableCollection<Route> _routes;

        public bool IsLoading
        {
            get { return _isLoading; }
            private set
            {
                _isLoading = value;
                OnPropertyChanged("IsLoading");
            }
        }

        public Stop SelectedStop
        {
            get { return _selectedStop; }
            set
            {
                _selectedStop = value;
                OnPropertyChanged("SelectedStop");
            }
        }

        public bool IsLoadingMovements
        {
            get { return _isLoadingMovements; }
            private set
            {
                _isLoadingMovements = value;
                OnPropertyChanged("IsLoadingMovements");
            }
        }

        public bool HasMovements
        {
            get { return _hasMovements; }
            private set
            {
                _hasMovements = value;
                OnPropertyChanged("HasMovements");
            }
        }

        public string MovementMessage
        {
            get { return _movementMessage; }
            private set
            {
                _movementMessage = value;
                OnPropertyChanged("MovementMessage");
            }
        }

        public DateTime RefreshTime
        {
            get { return _refreshTime; }
            private set
            {
                _refreshTime = value;
                OnPropertyChanged("RefreshTime");
            }
        }

        public ObservableCollection<Movement> Movements
        {
            get { return _movements; }
            private set
            {
                _movements = value;
                OnPropertyChanged("Movements");
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

        public DelegateCommand TapRefreshCommand { get; set; }
        public DelegateCommand<Route> TapRouteCommand { get; set; }
        public DelegateCommand TapCenterMapCommand { get; set; }

        public StopPageViewModel()
        {
            HasMovements = true;
            TapRefreshCommand = new DelegateCommand(ExecuteTapRefreshCommand);
            TapRouteCommand = new DelegateCommand<Route>(ExecuteTapRouteCommand, CanExecuteTapRouteCommand);
            TapCenterMapCommand = new DelegateCommand(ExecuteTapCenterMapCommand, CanExecuteTapCenterMapCommand);
        }

        public override async void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            IsLoading = true;
            try
            {
                var stopId = e.Parameter.ToString();
                var stop = await SqlService.GetStopById(stopId);
                SelectedStop = stop;

                await SetRoutes(SelectedStop.Id);

                var datetime = DateTime.UtcNow;
                RefreshTime = TimeZoneInfo.ConvertTime(datetime, TimeZoneInfo.FindSystemTimeZoneById("New Zealand Standard Time"));

                var movements = await GetLiveTimes(SelectedStop.Code);
                if (movements != null)
                    Movements = new ObservableCollection<Movement>(movements.OrderBy(x => x.ActualArrivalTime));

            }
            catch (Exception)
            {

            }
            finally
            {
                IsLoading = false;
            }
        }

        public async Task SetRoutes(string stopId)
        {
            var routes = await SqlService.GetRoutesByStopId(stopId);
            if (!routes.Any())
            {
                routes = await RestService.GetRoutesByStopId(stopId);
                routes = routes.OrderBy(x => x.AgencyId).ThenBy(x => x.ShortName);

                var latestRoutes = await SqlService.GetRoutes();
                var existRoutes = from r in routes
                                  join lr in latestRoutes
                                  on r.Id equals lr.Id
                                  select r;

                await SqlService.AddRouteStops(stopId, existRoutes.Select(x => x.Id));
                routes = existRoutes;
            }

            Routes = new ObservableCollection<Route>(routes);
        }

        private async Task<IEnumerable<Movement>> GetLiveTimes(int stopCode)
        {
            var response = await WebClientService.GetStopLiveData(stopCode);

            if (response == null)
            {
                MovementMessage = "cannot retrieve live data from auckland transport servers at this time";
                HasMovements = false;
            }
            else if (!response.Any())
            {
                MovementMessage = "no data available for this stop";
                HasMovements = false;
            }

            IsLoadingMovements = false;
            return response;
        }

        public async void ExecuteTapRefreshCommand()
        {
            var datetime = DateTime.UtcNow;
            RefreshTime = TimeZoneInfo.ConvertTime(datetime, TimeZoneInfo.FindSystemTimeZoneById("New Zealand Standard Time"));

            var movements = await GetLiveTimes(SelectedStop.Code);
            if (movements != null)
                Movements = new ObservableCollection<Movement>(movements.OrderBy(x => x.ActualArrivalTime));
        }

        private void ExecuteTapRouteCommand(Route route)
        {
            NavigationService.Navigate(App.Experiences.Route.ToString(), route.Id);
        }

        private bool CanExecuteTapRouteCommand(Route route)
        {
            return true;
        }

        private void ExecuteTapCenterMapCommand()
        {
            MessengerService.Send(true, "ReCenterMapStop");
        }

        private bool CanExecuteTapCenterMapCommand()
        {
            return true;
        }

        public Stop GetStop()
        {
            return SelectedStop;
        }
    }
}
