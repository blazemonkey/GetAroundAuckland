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
                        var command = new MySqlCommand("SELECT * FROM routes", conn);
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
            catch (Exception e)
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
            catch (Exception e)
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
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
