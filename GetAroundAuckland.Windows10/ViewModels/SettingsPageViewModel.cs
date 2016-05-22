using GetAroundAuckland.Windows10.Interfaces;
using Prism.Windows.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.ApplicationModel;
using Windows.UI.Xaml.Navigation;

namespace GetAroundAuckland.Windows10.ViewModels
{
    public class SettingsPageViewModel : BaseViewModel, ISettingsPageViewModel
    {
        private bool _gps;
        private ObservableCollection<string> _distances;
        private string _selectedDistance;
        private string _infoVersion;
        private string _version;
        private string _policyText;
        private string _policy2Text;

        public bool Gps
        {
            get { return _gps; }
            set
            {
                _gps = value;
                OnPropertyChanged("Gps");

                AppDataService.UpdateSettingsKeyValue<bool>("GPS", Gps);
            }
        }

        public ObservableCollection<string> Distances
        {
            get { return _distances; }
            private set
            {
                _distances = value;
                OnPropertyChanged("Distances");
            }
        }

        public string SelectedDistance
        {
            get { return _selectedDistance; }
            set
            {
                _selectedDistance = value;
                OnPropertyChanged("SelectedDistance");

                AppDataService.UpdateSettingsKeyValue<string>("DefaultDistance", SelectedDistance);
            }
        }

        public string InfoVersion
        {
            get { return _infoVersion; }
            set
            {
                _infoVersion = value;
                OnPropertyChanged("InfoVersion");
            }
        }

        public string Version
        {
            get { return _version; }
            set
            {
                _version = value;
                OnPropertyChanged("Version");
            }
        }

        public string PolicyText
        {
            get { return _policyText; }
            set
            {
                _policyText = value;
                OnPropertyChanged("PolicyText");
            }
        }

        public string Policy2Text
        {
            get { return _policy2Text; }
            set
            {
                _policy2Text = value;
                OnPropertyChanged("Policy2Text");
            }
        }

        public SettingsPageViewModel()
        {
            Distances = new ObservableCollection<string>();
            Distances.Add("500 m");
            Distances.Add("1 km");
            Distances.Add("2 km");
            Distances.Add("5 km");

            PolicyText = "While using our Get Around Auckland app, we may collect GPS data from your location to determine any nearby stops in your area. This GPS data will not be stored or transmitted, and will be automatically erased from memory every time the app is closed.";
            Policy2Text = "All information displayed has been gathered from the Auckland Transport API. Therefore Mosu Apps will not be responsible for the accuracy, availability, completeneess of the information and shall not have any legal liability for any loss resulting in the use of such information.";

            InfoVersion = "apr 2016";
        }

        public override async void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            SelectedDistance = AppDataService.GetSettingsKeyValue<string>("DefaultDistance");
            Gps = AppDataService.GetSettingsKeyValue<bool>("GPS");

            var file = await Package.Current.InstalledLocation.GetFileAsync("AppxManifest.xml");
            var appVersion = XDocument.Load("AppxManifest.xml").Root.Elements().Where(x => x.Name.LocalName == "Identity")
                                .First().Attributes().Where(x => x.Name.LocalName == "Version").First().Value;
            Version = appVersion;
        }

        public bool GetGps()
        {
            return Gps;
        }
    }
}
