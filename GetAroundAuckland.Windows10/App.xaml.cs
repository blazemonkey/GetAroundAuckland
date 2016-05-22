using System;
using System.Collections.Generic;
using System.Linq;
using Windows.ApplicationModel.Activation;
using Prism.Windows;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Services.FileReaderService;
using Services.JsonService;
using Services.MessengerService;
using GetAroundAuckland.Windows10.Services.SqlService;
using GetAroundAuckland.Windows10.Interfaces;
using GetAroundAuckland.Windows10.ViewModels;
using GetAroundAuckland.Windows10.Services.RestService;
using Services.NavigationService;
using GetAroundAuckland.Windows10.Services.AppDataService;
using GetAroundAuckland.Windows10.Services.WebClientService;

namespace GetAroundAuckland.Windows10
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public sealed partial class App : PrismApplication
    {
        public enum Experiences { Startup, Main, Route, Stop, Settings }

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>        

        public static readonly UnityContainer Container = new UnityContainer();

        public App()
        {
            Microsoft.ApplicationInsights.WindowsAppInitializer.InitializeAsync(
                Microsoft.ApplicationInsights.WindowsCollectors.Metadata |
                Microsoft.ApplicationInsights.WindowsCollectors.Session);
            this.InitializeComponent();
        }        

        protected override async Task OnInitializeAsync(IActivatedEventArgs args)
        {
            Container.RegisterInstance(NavigationService);
            Container.RegisterInstance<IMainPageViewModel>(new MainPageViewModel());
            Container.RegisterInstance<ISettingsPageViewModel>(new SettingsPageViewModel());
            Container.RegisterType<IAppDataService, AppDataService>();
            Container.RegisterType<IFileReaderService, FileReaderService>();
            Container.RegisterType<IJsonService, JsonService>();
            Container.RegisterType<IMessengerService, MessengerService>();
            Container.RegisterType<INavigationService, NavigationService>();
            Container.RegisterType<IRestService, RestService>();
            Container.RegisterType<ISqlService, SqlService>();
            Container.RegisterType<IWebClientService, WebClientService>();

            Container.Resolve<AppDataService>().InitializeAppDataContainer();
        }

        protected override object Resolve(Type type)
        {
            return Container.Resolve(type);
        }

        protected override Task OnLaunchApplicationAsync(LaunchActivatedEventArgs args)
        {
            NavigationService.Navigate(Experiences.Startup.ToString(), null);
            return Task.FromResult<object>(null);
        }
    }
}
