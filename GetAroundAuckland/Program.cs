using GetAroundAuckland.Services.ZipService;
using GetAroundAuckland.Services.WebClientService;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GetAroundAuckland.Services.CsvService;
using GetAroundAuckland.Services.SqlService;

namespace GetAroundAuckland
{
    class Program
    {
        private static Updater _updater;

        static void Main(string[] args)
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            ResolveTypes(container);

            _updater.Start();

            Console.WriteLine("Press any key to continue...");
            Console.ReadLine();
        }

        private static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<ICsvService, CsvService>();
            container.RegisterType<ISqlService, SqlService>();
            container.RegisterType<ISqlService, MySqlService>();
            container.RegisterType<IWebClientService, WebClientService>();
            container.RegisterType<IZipService, ZipService>();
        }

        private static void ResolveTypes(IUnityContainer container)
        {
            _updater = container.Resolve<Updater>();
        }

    }
}