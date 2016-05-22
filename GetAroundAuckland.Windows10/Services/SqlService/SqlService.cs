using GetAroundAuckland.Windows10.Models;
using SQLite.Net;
using SQLite.Net.Async;
using SQLite.Net.Platform.WinRT;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;

namespace GetAroundAuckland.Windows10.Services.SqlService
{
    public class SqlService : ISqlService
    {
        private readonly SQLiteAsyncConnection _conn;

        public SqlService()
        {
            var path = Path.Combine(ApplicationData.Current.LocalFolder.Path, "getaroundauckland.sqlite");
            _conn = new SQLiteAsyncConnection(() => new SQLiteConnectionWithLock(
                new SQLitePlatformWinRT(),
                new SQLiteConnectionString(path, false)));
        }

        public async Task InitDb()
        {
            await _conn.DropTableAsync<Route>();
            await _conn.CreateTableAsync<Agency>();
            await _conn.CreateTableAsync<Calendar>();
            await _conn.CreateTableAsync<CalendarDate>();
            await _conn.CreateTableAsync<Route>();
            await _conn.CreateTableAsync<RouteStop>();
            await _conn.CreateTableAsync<Stop>();
            await _conn.CreateTableAsync<StopTime>();
            await _conn.CreateTableAsync<Trip>();

            await _conn.ExecuteAsync("CREATE TABLE IF NOT EXISTS Shape ( " +
                      "Id, " +
                      "Latitude, " +
                      "Longitude, " +
                      "Sequence, " +
                      "Distance, " +
                      "PRIMARY KEY(Id, Sequence) " +
                      ")");
        }

        public async Task<IEnumerable<Agency>> GetAgencies()
        {
            return await _conn.Table<Agency>().ToListAsync();
        }

        public async Task AddAgencies(IEnumerable<Agency> agencies)
        {
            await _conn.InsertOrIgnoreAllAsync(agencies);
        }

        public async Task<IEnumerable<Route>> GetRoutes()
        {
            return await _conn.Table<Route>().Where(x => x.IsLatest).ToListAsync();
        }

        public async Task<Route> GetRouteById(string routeId)
        {
            return await _conn.Table<Route>().Where(x => x.Id == routeId).FirstOrDefaultAsync();
        }

        public async Task AddRoutes(IEnumerable<Route> routes)
        {
            await _conn.InsertOrIgnoreAllAsync(routes);
        }

        public async Task UpdateRoutesIsLatest()
        {
            await _conn.ExecuteAsync(@"UPDATE Route
                            SET IsLatest = 1
                            WHERE Id IN 
                            (SELECT MAX(Id) AS Id FROM 
		                            (SELECT Id, SUBSTR(Id,0, 6) AS LeftId, SUBSTR(Id,7) AS RightId FROM Route
		                            ORDER BY LeftId, RightId DESC
		                            ) AS A1
	                            GROUP BY LeftId);");
        }

        public async Task<IEnumerable<Stop>> GetStops()
        {
            return await _conn.Table<Stop>().ToListAsync();
        }

        public async Task<Stop> GetStopById(string stopId)
        {
            return await _conn.Table<Stop>().Where(x => x.Id == stopId).FirstOrDefaultAsync();
        }

        public async Task AddStops(IEnumerable<Stop> stops)
        {
            await _conn.InsertOrIgnoreAllAsync(stops);
        }

        public async Task<IEnumerable<Trip>> GetTripsByRouteId(string routeId)
        {
            return await _conn.Table<Trip>().Where(x => x.RouteId == routeId).ToListAsync();
        }

        public async Task AddTrips(IEnumerable<Trip> trips)
        {
            await _conn.InsertOrIgnoreAllAsync(trips);
        }

        public async Task<IEnumerable<StopTime>> GetStopTimesByTripId(string tripId)
        {
            return await _conn.Table<StopTime>().Where(x => x.TripId == tripId).ToListAsync();
        }

        public async Task AddStopTimes(IEnumerable<StopTime> stopTimes)
        {
            await _conn.InsertOrIgnoreAllAsync(stopTimes);
        }

        public async Task<IEnumerable<Calendar>> GetCalendarsByServiceId(string serviceId)
        {
            return await _conn.Table<Calendar>().Where(x => x.ServiceId == serviceId).ToListAsync();
        }

        public async Task AddCalendars(IEnumerable<Calendar> calendars)
        {
            await _conn.InsertOrIgnoreAllAsync(calendars);
        }

        public async Task<IEnumerable<CalendarDate>> GetCalendarDatesByServiceId(string serviceId)
        {
            return await _conn.Table<CalendarDate>().Where(x => x.ServiceId == serviceId).ToListAsync();
        }

        public async Task AddCalendarDates(IEnumerable<CalendarDate> calendarDates)
        {
            await _conn.InsertOrIgnoreAllAsync(calendarDates);
        }

        public async Task<IEnumerable<Shape>> GetShapesById(string shapeId)
        {
            return await _conn.Table<Shape>().Where(x => x.Id == shapeId).ToListAsync();
        }

        public async Task AddShapes(IEnumerable<Shape> shapes)
        {
            await _conn.InsertOrIgnoreAllAsync(shapes);
        }

        public async Task<IEnumerable<Route>> GetRoutesByStopId(string stopId)
        {
            var routeStops = await _conn.Table<RouteStop>().Where(x => x.StopId == stopId).ToListAsync();
            var routes = await GetRoutes();

            var routeStopsJoined = from route in routes
                                   join rs in routeStops
                                   on route.Id equals rs.RouteId
                                   orderby route.AgencyId, route.ShortName
                                   select route;

            return routeStopsJoined;
        }

        public async Task AddRouteStops(string stopId, IEnumerable<string> routeIds)
        {
            foreach (var id in routeIds)
            {
                var routeStop = new RouteStop() { RouteId = id, StopId = stopId };
                await _conn.InsertOrIgnoreAsync(routeStop);
            }
        }
    }
}
