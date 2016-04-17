using GetAroundAuckland.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetAroundAuckland.Services.SqlService
{
    public interface ISqlService
    {
        bool AddAgencies(List<Agency> agencies);
        bool AddCalendars(List<Calendar> calendars);
        bool AddCalendarDates(List<CalendarDate> calendarDates);
        bool AddRoutes(List<Route> routes);
        bool AddShapes(List<Shape> shapes);
        bool AddStops(List<Stop> stops);
        bool AddStopTimes(List<StopTime> stopTimes);
        bool AddTrips(List<Trip> trips);
    }
}
