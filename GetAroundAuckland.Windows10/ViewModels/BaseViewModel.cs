using GetAroundAuckland.Windows10.Interfaces;
using GetAroundAuckland.Windows10.Services.SqlService;
using Microsoft.Practices.Unity;
using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;
using Services.MessengerService;
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
        public ISqlService SqlService { get; set; }

        public void Navigate(Page page)
        {
            MessengerService.Send(page, "NavigationChanged");
        }
    }
}
