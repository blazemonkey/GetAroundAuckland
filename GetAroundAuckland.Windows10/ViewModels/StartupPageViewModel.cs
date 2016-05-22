using GetAroundAuckland.Windows10.Interfaces;
using Prism.Windows.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GetAroundAuckland.Windows10.App;

namespace GetAroundAuckland.Windows10.ViewModels
{
    public class StartupPageViewModel : BaseViewModel, IStartupPageViewModel
    {
        private string _status;
        private bool _isProgressBarVisible;
        private int _progress;

        public string Status
        {
            get { return _status; }
            private set
            {
                _status = value;
                OnPropertyChanged("Status");
            }
        }

        public bool IsProgressBarVisible
        {
            get { return _isProgressBarVisible; }
            private set
            {
                _isProgressBarVisible = value;
                OnPropertyChanged("IsProgressBarVisible");
            }
        }

        public int Progress
        {
            get { return _progress; }
            private set
            {
                _progress = value;
                OnPropertyChanged("Progress");
            }
        }

        public override async void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            Status = "checking local data";
            await SqlService.InitDb();

            //var lastUpdated = AppDataService.GetSettingsKeyValue<string>("LastUpdated");
            //if (DateTime.ParseExact(lastUpdated, "yyyyMMdd", null) >= DateTime.Now.Date)
            //    NavigationService.Navigate(Experiences.Main.ToString(), null);

            var success = await GetDataFromAT();
            if (success)
            {
                NavigationService.ClearHistory();
                NavigationService.Navigate(Experiences.Main.ToString(), null);
            }
        }

        private async Task<bool> GetDataFromAT()
        {
            try
            {
                IsProgressBarVisible = true;
                Status = "retrieving information from auckland transport";

                var agencies = await RestService.GetAgencies();
                if (agencies == null)
                    return false;

                await SqlService.AddAgencies(agencies);
                Progress = 25;

                var routes = await RestService.GetRoutes();
                if (routes == null)
                    return false;

                await SqlService.AddRoutes(routes);
                await SqlService.UpdateRoutesIsLatest();
                Progress = 50;

                var stops = await RestService.GetStops();
                if (stops == null)
                    return false;

                await SqlService.AddStops(stops);
                Progress = 75;

                var calendarDates = await WebClientService.GetCalendarDates();
                if (calendarDates == null)
                    return false;

                await SqlService.AddCalendarDates(calendarDates);
                Progress = 100;

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }        
    }
}
