using GetAroundAuckland.Windows10.Interfaces;
using GetAroundAuckland.Windows10.Services.RestService;
using GetAroundAuckland.Windows10.Services.SqlService;
using Microsoft.Practices.Unity;
using Prism.Windows.Mvvm;
using Services.MessengerService;
using Services.NavigationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace GetAroundAuckland.Windows10.ViewModels
{
    public class BaseViewModel : ViewModelBase
    {
        [Dependency]
        public IMainPageViewModel MainPageViewModel { get; set; }
        [Dependency]
        public INavigationService NavigationService { get; set; }
        [Dependency]
        public IMessengerService MessengerService { get; set; }
        [Dependency]
        public IRestService RestService { get; set; }
        [Dependency]
        public ISqlService SqlService { get; set; }

        public void Navigate(Page page)
        {
            MessengerService.Send(page, "NavigationChanged");
        }
    }
}
