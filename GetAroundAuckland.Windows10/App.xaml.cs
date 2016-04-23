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

namespace GetAroundAuckland.Windows10
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public sealed partial class App : PrismApplication
    {
        public enum Experiences { Main, Route, Stop }

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
            Container.RegisterType<IFileReaderService, FileReaderService>();
            Container.RegisterType<IJsonService, JsonService>();
            Container.RegisterType<IMessengerService, MessengerService>();
            Container.RegisterType<ISqlService, SqlService>();
        }

        protected override object Resolve(Type type)
        {
            return Container.Resolve(type);
        }

        protected override Task OnLaunchApplicationAsync(LaunchActivatedEventArgs args)
        {
            NavigationService.Navigate(Experiences.Main.ToString(), null);
            return Task.FromResult<object>(null);
        }
    }
}
