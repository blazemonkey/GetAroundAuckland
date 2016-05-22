using GetAroundAuckland.Windows10.Interfaces;
using GetAroundAuckland.Windows10.Services.RestService;
using GetAroundAuckland.Windows10.Services.SqlService;
using Microsoft.Practices.Unity;
using Prism.Windows.Mvvm;
using GetAroundAuckland.Windows10.Services.AppDataService;
using Services.MessengerService;
using Services.NavigationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using GetAroundAuckland.Windows10.Services.WebClientService;

namespace GetAroundAuckland.Windows10.ViewModels
{
    public class BaseViewModel : ViewModelBase
    {
        [Dependency]
        public IAppDataService AppDataService { get; set; }
        [Dependency]
        public IMainPageViewModel MainPageViewModel { get; set; }
        [Dependency]
        public ISettingsPageViewModel SettingsPageViewModel { get; set; }
        [Dependency]
        public INavigationService NavigationService { get; set; }
        [Dependency]
        public IMessengerService MessengerService { get; set; }
        [Dependency]
        public IRestService RestService { get; set; }
        [Dependency]
        public ISqlService SqlService { get; set; }
        [Dependency]
        public IWebClientService WebClientService { get; set; }

        public void Navigate(Page page)
        {
            MessengerService.Send(page, "NavigationChanged");
        }
    }
}
