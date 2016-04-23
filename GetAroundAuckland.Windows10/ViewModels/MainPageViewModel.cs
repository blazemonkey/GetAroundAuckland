using GetAroundAuckland.Windows10.Interfaces;
using GetAroundAuckland.Windows10.Views;
using Prism.Commands;
using Prism.Windows.Mvvm;
using System;
using System.Collections.Generic;
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

        public DelegateCommand MenuClickCommand { get; set; }
        public DelegateCommand RoutesClickCommand { get; set; }
        public DelegateCommand StopsClickCommand { get; set; }
        public DelegateCommand NearbyClickCommand { get; set; }

        public MainPageViewModel()
        {
            MenuClickCommand = new DelegateCommand(ExecuteMenuClickCommand, CanExecuteMenuClickCommand);
            RoutesClickCommand = new DelegateCommand(ExecuteRoutesClickCommand, CanExecuteRoutesClickCommand);
            StopsClickCommand = new DelegateCommand(ExecuteStopsClickCommand, CanExecuteStopsClickCommand);
            NearbyClickCommand = new DelegateCommand(ExecuteNearbyClickCommand, CanExecuteNearbyClickCommand);
        }

        public override void OnNavigatedTo(Prism.Windows.Navigation.NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
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

        }

        public bool CanExecuteStopsClickCommand()
        {
            return true;
        }

        public void ExecuteNearbyClickCommand()
        {

        }

        public bool CanExecuteNearbyClickCommand()
        {
            return true;
        }
    }
}
