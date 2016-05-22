using GetAroundAuckland.Services.ZipService;
using GetAroundAuckland.Services.WebClientService;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CsvHelper;
using GetAroundAuckland.Models;
using GetAroundAuckland.Services.CsvService;
using GetAroundAuckland.Services.SqlService;

namespace GetAroundAuckland
{
    public class Updater
    {
        public const string AT_GTFS_PATH = "https://cdn01.at.govt.nz/data/gtfs.zip";
        public const int TOTAL_RETRY = 5;
        public const string ZIP_PATH = "gtfs.zip";

        private int _retryNum;

        [Dependency]
        public ICsvService CsvService { get; set; }

        [Dependency]
        public ISqlService SqlService { get; set; }

        [Dependency]
        public IWebClientService WebClientService { get; set; }

        [Dependency]
        public IZipService ZipService { get; set; }

        public bool Start()
        {
            //var download = WebClientService.DownloadFile(AT_GTFS_PATH, ZIP_PATH);
            //if (!download)
            //{
            //    Logger.Info(string.Format("File could not be downloaded. Retrying ({0})", _retryNum++));
            //    if (_retryNum <= TOTAL_RETRY)
            //        Start();
            //    else
            //        return false;
            //}

            //var zip = ZipService.Unzip(ZIP_PATH, "gtfs");
            //if (!zip)
            //{
            //    Logger.Info("Zip file could not be extracted.");
            //    return false;
            //}

            //var agencies = CsvService.Parse<Agency, AgencyMap>("gtfs\\agency.txt");
            //var calendars = CsvService.Parse<Calendar, CalendarMap>("gtfs\\calendar.txt");
            //var calendarDates = CsvService.Parse<CalendarDate, CalendarDateMap>("gtfs\\calendar_dates.txt");
            //var routes = CsvService.Parse<Route, RouteMap>("gtfs\\routes.txt");
            //var shapes = CsvService.Parse<Shape, ShapeMap>("gtfs\\shapes.txt");
            //var stops = CsvService.Parse<Stop, StopMap>("gtfs\\stops.txt");
            //var stopTimes = CsvService.Parse<StopTime, StopTimeMap>("gtfs\\stop_times.txt");
            //var trips = CsvService.Parse<Trip, TripMap>("gtfs\\trips.txt");

            //SqlService.AddAgencies(agencies);
            //SqlService.AddCalendars(calendars);
            //SqlService.AddCalendarDates(calendarDates);
            //SqlService.AddRoutes(routes);
            //SqlService.AddShapes(shapes);
            //SqlService.AddStops(stops);
            //SqlService.AddStopTimes(stopTimes);
            //SqlService.AddTrips(trips);

            SqlService.PostProcessing();

            return true;
        }
    }
}
