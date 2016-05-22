using GetAroundAuckland.Windows10.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GetAroundAuckland.Windows10.Services.RestService
{
    public interface IRestService
    {
        Task<IEnumerable<Agency>> GetAgencies();
        Task<IEnumerable<Route>> GetRoutes();
        Task<IEnumerable<Stop>> GetStops();
        Task<IEnumerable<Trip>> GetTripsByRouteId(string routeId);
        Task<IEnumerable<StopTime>> GetStopTimesByTripId(string tripId);
        Task<IEnumerable<Calendar>> GetCalendarsByServiceId(string serviceId);
        Task<IEnumerable<CalendarDate>> GetCalendarDatesByServiceId(string serviceId);
        Task<IEnumerable<Shape>> GetShapesById(string shapeId);
        Task<IEnumerable<Route>> GetRoutesByStopId(string stopId);
    }
}
