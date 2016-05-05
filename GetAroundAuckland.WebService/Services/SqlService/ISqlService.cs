using GetAroundAuckland.WebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetAroundAuckland.WebService.Services.SqlService
{
    public interface ISqlService
    {
        List<Route> GetRoutes();
        List<Stop> GetStops();
        List<Agency> GetAgencies();
        List<Trip> GetTripsByRouteId(string id);
        List<StopTime> GetStopTimesByTripId(string id);
        List<Shape> GetShapesById(string id);
        List<Calendar> GetCalendarsByServiceId(string id);
        List<CalendarDate> GetCalendarDatesByServiceId(string id);
    }
}
