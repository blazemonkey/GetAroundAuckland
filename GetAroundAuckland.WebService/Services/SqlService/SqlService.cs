using GetAroundAuckland.WebService.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace GetAroundAuckland.WebService.Services.SqlService
{
    public class SqlService : ISqlService
    {
        //public readonly string CONNECTION_STRING = ConfigurationManager.ConnectionStrings["mySqlConnString"].ConnectionString;
        public readonly string CONNECTION_STRING = "Server=localhost;Database=auckland_transport;Uid=gary;Pwd=serpents";

        public List<Route> GetRoutes()
        {
            try
            {
                using (var conn = new MySqlConnection(CONNECTION_STRING))
                {
                    var routes = new List<Route>();
                    conn.Open();

                    using (var trans = conn.BeginTransaction())
                    {
                        var command = new MySqlCommand("SELECT * FROM routes WHERE IsLatest = 1", conn);
                        command.Transaction = trans;
                        var reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            var route = new Route();
                            route.Id = reader.GetString(0).TrimEnd();
                            route.LongName = reader.GetString(1).TrimEnd();
                            route.ShortName = reader.GetString(2).TrimEnd();
                            route.Type = reader.GetByte(3);
                            route.TextColor = reader.GetString(4).TrimEnd();
                            route.Color = reader.GetString(5).TrimEnd();
                            route.AgencyId = reader.GetString(6).TrimEnd();
                            routes.Add(route);
                        }

                        conn.Close();
                    }

                    return routes;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Stop> GetStops()
        {
            try
            {
                using (var conn = new MySqlConnection(CONNECTION_STRING))
                {
                    var stops = new List<Stop>();
                    conn.Open();

                    using (var trans = conn.BeginTransaction())
                    {
                        var command = new MySqlCommand("SELECT * FROM stops", conn);
                        command.Transaction = trans;
                        var reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            var stop = new Stop();
                            stop.Id = reader.GetString(0).TrimEnd();
                            stop.Name = reader.GetString(1).TrimEnd();
                            stop.Description = reader.GetString(2).TrimEnd();
                            stop.Latitude = reader.GetDecimal(3);
                            stop.Longitude = reader.GetDecimal(4);
                            stop.ZoneId = reader.GetString(5).TrimEnd();
                            stop.Code = reader.GetInt32(6);
                            stop.LocationType = reader.GetByte(7);
                            stop.ParentStation = reader.GetString(8).TrimEnd();
                            stops.Add(stop);
                        }

                        conn.Close();
                    }

                    return stops;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Agency> GetAgencies()
        {
            try
            {
                using (var conn = new MySqlConnection(CONNECTION_STRING))
                {
                    var agencies = new List<Agency>();
                    conn.Open();

                    using (var trans = conn.BeginTransaction())
                    {
                        var command = new MySqlCommand("SELECT * FROM agencies", conn);
                        command.Transaction = trans;
                        var reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            var agency = new Agency();
                            agency.Id = reader.GetString(0).TrimEnd();
                            agency.Name = reader.GetString(1).TrimEnd();
                            agency.Url = reader.GetString(2).TrimEnd();
                            agency.TimeZone = reader.GetString(3).TrimEnd();
                            agency.Lang = reader.GetString(4).TrimEnd();
                            agency.Phone = reader.GetString(5).TrimEnd();
                            agencies.Add(agency);
                        }

                        conn.Close();
                    }

                    return agencies;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Trip> GetTripsByRouteId(string id)
        {
            try
            {
                using (var conn = new MySqlConnection(CONNECTION_STRING))
                {
                    var trips = new List<Trip>();
                    conn.Open();

                    using (var trans = conn.BeginTransaction())
                    {
                        var command = new MySqlCommand("SELECT * FROM trips WHERE RouteId = @RouteId", conn);
                        command.Transaction = trans;
                        command.Parameters.Add(new MySqlParameter("@RouteId", id));
                        var reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            var trip = new Trip();
                            trip.Id = reader.GetString(0).TrimEnd();
                            trip.RouteId = reader.GetString(1).TrimEnd();
                            trip.ServiceId = reader.GetString(2).TrimEnd();
                            trip.Headsign = reader.GetString(3).TrimEnd();
                            trip.DirectionId = reader.GetInt16(4);
                            trip.BlockId = reader.GetString(5).TrimEnd();
                            trip.ShapeId = reader.GetString(6).TrimEnd();
                            trip.FirstArrivalTime = reader.GetString(7).TrimEnd();
                            trip.LastDepartureTime = reader.GetString(8).TrimEnd();
                            trips.Add(trip);
                        }

                        conn.Close();
                    }

                    return trips;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<StopTime> GetStopTimesByTripId(string id)
        {
            try
            {
                using (var conn = new MySqlConnection(CONNECTION_STRING))
                {
                    var stopTimes = new List<StopTime>();
                    conn.Open();

                    using (var trans = conn.BeginTransaction())
                    {
                        var command = new MySqlCommand("SELECT * FROM stop_times WHERE TripId = @TripId", conn);
                        command.Transaction = trans;
                        command.Parameters.Add(new MySqlParameter("@TripId", id));
                        var reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            var stopTime = new StopTime();
                            stopTime.TripId = reader.GetString(0).TrimEnd();
                            stopTime.ArrivalTime = reader.GetString(1).TrimEnd();
                            stopTime.DepartureTime = reader.GetString(2).TrimEnd();
                            stopTime.StopId = reader.GetString(3).TrimEnd();
                            stopTime.StopSequence = reader.GetInt32(4);
                            stopTime.StopHeadsign = reader.GetString(5).TrimEnd();
                            if (reader.IsDBNull(6))
                                stopTime.PickupType = null;
                            else
                                stopTime.PickupType = reader.GetInt32(6);
                            if (reader.IsDBNull(7))
                                stopTime.DropOffType = null;
                            else
                                stopTime.DropOffType = reader.GetInt32(7);
                            if (reader.IsDBNull(8))
                                stopTime.ShapeDistance = null;
                            else
                                stopTime.ShapeDistance = reader.GetInt32(4);
                            stopTimes.Add(stopTime);
                        }

                        conn.Close();
                    }

                    return stopTimes;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Shape> GetShapesById(string id)
        {
            try
            {
                using (var conn = new MySqlConnection(CONNECTION_STRING))
                {
                    var shapes = new List<Shape>();
                    conn.Open();

                    using (var trans = conn.BeginTransaction())
                    {
                        var command = new MySqlCommand("SELECT * FROM shapes WHERE Id = @Id", conn);
                        command.Transaction = trans;
                        command.Parameters.Add(new MySqlParameter("@Id", id));
                        var reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            var shape = new Shape();
                            shape.Id = reader.GetString(0).TrimEnd();
                            shape.Latitude = reader.GetDecimal(1);
                            shape.Longitude = reader.GetDecimal(2);
                            shape.Sequence = reader.GetInt32(3);
                            if (reader.IsDBNull(4))
                                shape.Distance = null;
                            else
                                shape.Distance = reader.GetInt32(4);
                            shapes.Add(shape);
                        }

                        conn.Close();
                    }

                    return shapes;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Calendar> GetCalendarsByServiceId(string id)
        {
            try
            {
                using (var conn = new MySqlConnection(CONNECTION_STRING))
                {
                    var calendars = new List<Calendar>();
                    conn.Open();

                    using (var trans = conn.BeginTransaction())
                    {
                        var command = new MySqlCommand("SELECT * FROM calendars WHERE ServiceId = @ServiceId", conn);
                        command.Transaction = trans;
                        command.Parameters.Add(new MySqlParameter("@ServiceId", id));
                        var reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            var calendar = new Calendar();
                            calendar.ServiceId = reader.GetString(0).TrimEnd();
                            calendar.StartDate = reader.GetString(1).TrimEnd();
                            calendar.EndDate = reader.GetString(2).TrimEnd();
                            calendar.Monday = reader.GetBoolean(3);
                            calendar.Tuesday = reader.GetBoolean(4);
                            calendar.Wednesday = reader.GetBoolean(5);
                            calendar.Thursday = reader.GetBoolean(6);
                            calendar.Friday = reader.GetBoolean(7);
                            calendar.Saturday = reader.GetBoolean(8);
                            calendar.Sunday = reader.GetBoolean(9);
                            calendars.Add(calendar);
                        }

                        conn.Close();
                    }

                    return calendars;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<CalendarDate> GetCalendarDatesByServiceId(string id)
        {
            try
            {
                using (var conn = new MySqlConnection(CONNECTION_STRING))
                {
                    var calendarDates = new List<CalendarDate>();
                    conn.Open();

                    using (var trans = conn.BeginTransaction())
                    {
                        var command = new MySqlCommand("SELECT * FROM calendar_dates WHERE ServiceId = @ServiceId", conn);
                        command.Transaction = trans;
                        command.Parameters.Add(new MySqlParameter("@ServiceId", id));
                        var reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            var calendarDate = new CalendarDate();
                            calendarDate.ServiceId = reader.GetString(0).TrimEnd();
                            calendarDate.Date = reader.GetString(1).TrimEnd();
                            calendarDate.ExceptionType = reader.GetByte(2);
                            calendarDates.Add(calendarDate);
                        }

                        conn.Close();
                    }

                    return calendarDates;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
