using GetAroundAuckland.Windows10.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetAroundAuckland.Windows10.Services.SqlService
{
    public interface ISqlService
    {
        Task InitDb();
        Task<IEnumerable<Agency>> GetAgencies();
        Task AddAgencies(IEnumerable<Agency> agencies);
        Task<IEnumerable<Route>> GetRoutes();
        Task<Route> GetRouteById(string routeId);
        Task AddRoutes(IEnumerable<Route> routes);
        Task UpdateRoutesIsLatest();
        Task<IEnumerable<Stop>> GetStops();
        Task<Stop> GetStopById(string stopId);
        Task AddStops(IEnumerable<Stop> stops);
        Task<IEnumerable<Trip>> GetTripsByRouteId(string routeId);
        Task AddTrips(IEnumerable<Trip> trips);
        Task<IEnumerable<StopTime>> GetStopTimesByTripId(string tripId);
        Task AddStopTimes(IEnumerable<StopTime> stopTimes);
        Task<IEnumerable<Calendar>> GetCalendarsByServiceId(string serviceId);
        Task AddCalendars(IEnumerable<Calendar> calendars);
        Task<IEnumerable<CalendarDate>> GetCalendarDatesByServiceId(string serviceId);
        Task AddCalendarDates(IEnumerable<CalendarDate> calendarDates);
        Task<IEnumerable<Shape>> GetShapesById(string shapeId);
        Task AddShapes(IEnumerable<Shape> shapes);
        Task<IEnumerable<Route>> GetRoutesByStopId(string stopId);
        Task AddRouteStops(string stopId, IEnumerable<string> routeIds);
    }
}
