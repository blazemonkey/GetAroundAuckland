using Microsoft.Practices.Unity;
using Services.MessengerService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace GetAroundAuckland.Windows10.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        [Dependency]
        public IMessengerService MessengerService { get; set; }
        public MainPage()
        {
            this.InitializeComponent();

            MessengerService = App.Container.Resolve<MessengerService>();
            MessengerService.Register<Page>(this, "NavigationChanged", x => NavigationChanged(x));
        }

        private void NavigationChanged(Page page)
        {
            if (SplitViewContent.Content != null && SplitViewContent.Content.GetType() == page.GetType())
                return;

            SplitViewContent.Navigate(page.GetType());
        }
    }
}
