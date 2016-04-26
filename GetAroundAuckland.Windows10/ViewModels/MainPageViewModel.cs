﻿using GetAroundAuckland.Windows10.Interfaces;
using GetAroundAuckland.Windows10.Models;
using GetAroundAuckland.Windows10.Views;
using Prism.Commands;
using Prism.Windows.Mvvm;
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
        private bool _isPaneOpen;
        private bool _isRouteChecked;
        private bool _isStopChecked;
        private bool _isNearbyChecked;

        private bool _isLoading;
        private ObservableCollection<Agency> _agencies;
        private Agency _selectedAgency;
        private ObservableCollection<RouteType> _routeTypes;
        private RouteType _selectedRouteType;
        private ObservableCollection<Route> _routes;
        private ObservableCollection<Stop> _stops;

        public bool IsPaneOpen
        {
            get { return _isPaneOpen; }
            private set
            {
                _isPaneOpen = value;
                OnPropertyChanged("IsPaneOpen");
            }
        }

        public bool IsRouteChecked
        {
            get { return _isRouteChecked; }
            private set
            {
                _isRouteChecked = value;
                OnPropertyChanged("IsRouteChecked");
                if (value)
                    ExecuteRoutesClickCommand();
            }
        }

        public bool IsStopChecked
        {
            get { return _isStopChecked; }
            private set
            {
                _isStopChecked = value;
                OnPropertyChanged("IsStopChecked");
                if (value)
                    ExecuteStopsClickCommand();
            }
        }

        public bool IsNearbyChecked
        {
            get { return _isNearbyChecked; }
            private set
            {
                _isNearbyChecked = value;
                OnPropertyChanged("IsNearbyChecked");
                if (value)
                    ExecuteNearbyClickCommand();
            }
        }

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

        public DelegateCommand MenuClickCommand { get; set; }
        public DelegateCommand RoutesClickCommand { get; set; }
        public DelegateCommand StopsClickCommand { get; set; }
        public DelegateCommand NearbyClickCommand { get; set; }
        public DelegateCommand TapSearchRoutesCommand { get; set; }

        public MainPageViewModel()
        {
            MenuClickCommand = new DelegateCommand(ExecuteMenuClickCommand, CanExecuteMenuClickCommand);
            RoutesClickCommand = new DelegateCommand(ExecuteRoutesClickCommand, CanExecuteRoutesClickCommand);
            StopsClickCommand = new DelegateCommand(ExecuteStopsClickCommand, CanExecuteStopsClickCommand);
            NearbyClickCommand = new DelegateCommand(ExecuteNearbyClickCommand, CanExecuteNearbyClickCommand);
            TapSearchRoutesCommand = new DelegateCommand(ExecuteTapSearchRoutesCommand, CanExecuteTapSearchRoutesCommand);
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
            }
            catch (Exception)
            {

            }
            finally
            {
                IsLoading = false;
            }

            if (e.NavigationMode == NavigationMode.New)
            {
                IsRouteChecked = true;
            }
            else
            {
                IsRouteChecked = false;
            }
        }

        public void ExecuteMenuClickCommand()
        {
            IsPaneOpen = !IsPaneOpen;
        }

        public bool CanExecuteMenuClickCommand()
        {
            return true;
        }

        public void ExecuteRoutesClickCommand()
        {
            Navigate(new RoutesPage());
        }

        public bool CanExecuteRoutesClickCommand()
        {
            return true;
        }

        public void ExecuteStopsClickCommand()
        {
            Navigate(new StopsPage());
        }

        public bool CanExecuteStopsClickCommand()
        {
            return true;
        }

        public void ExecuteNearbyClickCommand()
        {
            Navigate(new NearbyPage());
        }

        public bool CanExecuteNearbyClickCommand()
        {
            return true;
        }

        public void ExecuteTapSearchRoutesCommand()
        {

        }

        public bool CanExecuteTapSearchRoutesCommand()
        {
            return true;
        }

        public List<Route> FilterRoutes(string routeNo)
        {            
            return Routes.Where(x => x.ShortName.Contains(routeNo)).ToList();           
        }

        public List<Stop> FilterStops(string stopNo)
        {
            return Stops.Where(x => x.Id.Contains(stopNo)).ToList();
        }
    }
}
