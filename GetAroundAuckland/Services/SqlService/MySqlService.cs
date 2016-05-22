using GetAroundAuckland.Services.SqlService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GetAroundAuckland.Models;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace GetAroundAuckland.Services.SqlService
{
    public class MySqlService : ISqlService
    {
        public readonly string CONNECTION_STRING = ConfigurationManager.ConnectionStrings["mySqlConnString"].ConnectionString;

        private bool Add<T>(List<T> rows, string selectSql, string insertSql, string updateSql) where T : DbModel
        {
            if (rows == null)
                return false;

            using (var conn = new MySqlConnection(CONNECTION_STRING))
            {
                try
                {
                    conn.Open();

                    using (var trans = conn.BeginTransaction())
                    {
                        try
                        {
                            foreach (var model in rows)
                            {
                                var selectCmd = new MySqlCommand(selectSql, conn);
                                selectCmd.Transaction = trans;
                                model.SetMySqlParameters(selectCmd, "Select");
                                var selectReader = selectCmd.ExecuteReader();

                                if (!selectReader.HasRows)
                                {
                                    selectReader.Close();

                                    var insertCmd = new MySqlCommand(insertSql, conn);
                                    insertCmd.Transaction = trans;
                                    model.SetMySqlParameters(insertCmd, "Insert");
                                    insertCmd.ExecuteNonQuery();
                                }
                                else
                                {
                                    var cmp = model.Compare(selectReader, model);
                                    selectReader.Close();

                                    if (cmp)
                                    {
                                        var updateCmd = new MySqlCommand(updateSql, conn);
                                        updateCmd.Transaction = trans;
                                        model.SetMySqlParameters(updateCmd, "Update");
                                        updateCmd.ExecuteNonQuery();
                                    }
                                }
                            }

                            trans.Commit();
                        }
                        catch (Exception e)
                        {
                            Logger.Error(e);
                            trans.Rollback();
                            throw;
                        }
                    }


                    return true;
                }
                catch (Exception e)
                {
                    Logger.Error(e);
                    return false;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public bool AddAgencies(List<Agency> agencies)
        {
            var stopWatch = new Stopwatch();
            Logger.Info("Inserting Agencies");
            stopWatch.Start();

            var result = Add(agencies, Agency.SelectSql, Agency.InsertSql, Agency.UpdateSql);

            stopWatch.Stop();
            Logger.Info("Finished Insert.");
            Logger.Info("Time Taken (ms): " + stopWatch.ElapsedMilliseconds);
            return result;
        }

        public bool AddCalendars(List<Calendar> calendars)
        {
            var stopWatch = new Stopwatch();
            Logger.Info("Inserting Calendars");
            stopWatch.Start();

            var result = Add(calendars, Calendar.SelectSql, Calendar.InsertSql, Calendar.UpdateSql);

            stopWatch.Stop();
            Logger.Info("Finished Insert.");
            Logger.Info("Time Taken (ms): " + stopWatch.ElapsedMilliseconds);
            return result;
        }

        public bool AddCalendarDates(List<CalendarDate> calendarDates)
        {
            var stopWatch = new Stopwatch();
            Logger.Info("Inserting Calendar Dates");
            stopWatch.Start();

            var result = Add(calendarDates, CalendarDate.SelectSql, CalendarDate.InsertSql, CalendarDate.UpdateSql);

            stopWatch.Stop();
            Logger.Info("Finished Insert.");
            Logger.Info("Time Taken (ms): " + stopWatch.ElapsedMilliseconds);
            return result;
        }

        public bool AddRoutes(List<Route> routes)
        {
            var stopWatch = new Stopwatch();
            Logger.Info("Inserting Routes");
            stopWatch.Start();

            var result = Add(routes, Route.SelectSql, Route.InsertSql, Route.UpdateSql);

            stopWatch.Stop();
            Logger.Info("Finished Insert.");
            Logger.Info("Time Taken (ms): " + stopWatch.ElapsedMilliseconds);
            return result;
        }

        public bool AddShapes(List<Shape> shapes)
        {
            var stopWatch = new Stopwatch();
            Logger.Info("Inserting Shapes");
            stopWatch.Start();

            var result = Add(shapes, Shape.SelectSql, Shape.InsertSql, Shape.UpdateSql);

            stopWatch.Stop();
            Logger.Info("Finished Insert.");
            Logger.Info("Time Taken (ms): " + stopWatch.ElapsedMilliseconds);
            return result;
        }

        public bool AddStops(List<Stop> stops)
        {
            var stopWatch = new Stopwatch();
            Logger.Info("Inserting Stops");
            stopWatch.Start();

            var result = Add(stops, Stop.SelectSql, Stop.InsertSql, Stop.UpdateSql);

            stopWatch.Stop();
            Logger.Info("Finished Insert.");
            Logger.Info("Time Taken (ms): " + stopWatch.ElapsedMilliseconds);
            return result;
        }

        public bool AddStopTimes(List<StopTime> stopTimes)
        {
            var stopWatch = new Stopwatch();
            Logger.Info("Inserting Stop Times");
            stopWatch.Start();

            var result = Add(stopTimes, StopTime.SelectSql, StopTime.InsertSql, StopTime.UpdateSql);

            stopWatch.Stop();
            Logger.Info("Finished Insert.");
            Logger.Info("Time Taken (ms): " + stopWatch.ElapsedMilliseconds);
            return result;
        }

        public bool AddTrips(List<Trip> trips)
        {
            var stopWatch = new Stopwatch();
            Logger.Info("Inserting Trips");
            stopWatch.Start();

            var result = Add(trips, Trip.SelectSql, Trip.InsertSql, Trip.UpdateSql);

            stopWatch.Stop();
            Logger.Info("Finished Insert.");
            Logger.Info("Time Taken (ms): " + stopWatch.ElapsedMilliseconds);
            return result;
        }

        public bool PostProcessing()
        {
            var stopWatch = new Stopwatch();
            Logger.Info("Post Processing");
            stopWatch.Start();

            using (var conn = new MySqlConnection(CONNECTION_STRING))
            {
                try
                {
                    conn.Open();

                    var selectSql = "SELECT * FROM trips";
                    var selectCmd = new MySqlCommand(selectSql, conn);
                    var selectReader = selectCmd.ExecuteReader();
                    var tripIds = new List<string>();

                    while (selectReader.Read())
                    {
                        var tripId = selectReader.GetString(0).TrimEnd();
                        tripIds.Add(tripId);
                    }

                    selectReader.Close();

                    var stopSelectSql = "SELECT * FROM stop_times WHERE tripId = @0 ORDER BY StopSequence";
                    var updateSql = "UPDATE trips SET FirstArrivalTime = @1, LastDepartureTime = @2 WHERE Id = @0";

                    foreach (var id in tripIds)
                    {
                        var stopSelectCmd = new MySqlCommand(stopSelectSql, conn);
                        stopSelectCmd.Parameters.Add(new MySqlParameter("@0", id));
                        var stopSelectReader = stopSelectCmd.ExecuteReader();
                        var arrivalTimes = new List<string>();
                        var departureTimes = new List<string>();

                        while (stopSelectReader.Read())
                        {
                            var arrival = stopSelectReader.GetString(1).TrimEnd();
                            var departure = stopSelectReader.GetString(2).TrimEnd();
                            arrivalTimes.Add(arrival);
                            departureTimes.Add(departure);
                        }

                        stopSelectReader.Close();

                        if (!(arrivalTimes.Any() && departureTimes.Any()))
                            continue;

                        var firstArrival = arrivalTimes.First();
                        var lastDeparture = departureTimes.Last();

                        var updateCmd = new MySqlCommand(updateSql, conn);
                        updateCmd.Parameters.Add(new MySqlParameter("@0", id));
                        updateCmd.Parameters.Add(new MySqlParameter("@1", firstArrival));
                        updateCmd.Parameters.Add(new MySqlParameter("@2", lastDeparture));
                        updateCmd.ExecuteNonQuery();                        
                    }

                    var updateIsLatestSql = @"SET SQL_SAFE_UPDATES = 0;
                            UPDATE routes SET IsLatest = 0;
                            UPDATE routes AS r
                            JOIN 
	                            (SELECT MAX(Id) AS Id FROM 
		                            (SELECT Id, SUBSTRING_INDEX(Id,'-', 1) AS LeftId, SUBSTRING_INDEX(Id,'-', -1) AS RightId FROM routes
		                            ORDER BY LeftId, RightId DESC
		                            ) AS A1
	                            GROUP BY LeftId) AS A2
                            ON r.Id = A2.Id
                            SET IsLatest = 1;
                            SET SQL_SAFE_UPDATES = 1;
                            ";
                    var updateIsLatestCmd = new MySqlCommand(updateIsLatestSql, conn);
                    updateIsLatestCmd.ExecuteNonQuery();

                    return true;
                }
                catch (Exception e)
                {
                    Logger.Error(e);
                    return false;
                }
                finally
                {
                    conn.Close();
                    stopWatch.Stop();
                    Logger.Info("Finished Post Processing.");
                    Logger.Info("Time Taken (ms): " + stopWatch.ElapsedMilliseconds);
                }
            }
        }
    }
}
