using CsvHelper.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetAroundAuckland.Models
{
    public class Trip : DbModel
    {
        public static string SelectSql = "SELECT * FROM trips WHERE Id = @0";
        public static string InsertSql = "INSERT INTO trips(Id, RouteId, ServiceId, Headsign, DirectionId, BlockId, ShapeId, CreatedTime, LastUpdatedTime) " +
                                        "VALUES (@0, @1, @2, @3, @4, @5, @6, @7, @8)";
        public static string UpdateSql = "UPDATE trips SET RouteId = @1, ServiceId = @2, Headsign = @3, DirectionId = @4, " +
                                         "BlockId = @5, ShapeId = @6, LastUpdatedTime = @7  WHERE Id = @0";

        public string Id { get; set; }
        public string RouteId { get; set; }
        public string ServiceId { get; set; }
        public string Headsign { get; set; }
        public int DirectionId { get; set; }
        public string BlockId { get; set; }
        public string ShapeId { get; set; }

        public override void SetSqlParameters(DbCommand command, string type)
        {
            var now = DateTime.UtcNow;

            switch (type)
            {
                case "Select":
                    {
                        command.Parameters.Add(new SqlParameter("@0", Id));
                        break;
                    }
                case "Insert":
                    {
                        command.Parameters.Add(new SqlParameter("@0", Id));
                        command.Parameters.Add(new SqlParameter("@1", RouteId));
                        command.Parameters.Add(new SqlParameter("@2", ServiceId));
                        command.Parameters.Add(new SqlParameter("@3", Headsign));
                        command.Parameters.Add(new SqlParameter("@4", DirectionId));
                        command.Parameters.Add(new SqlParameter("@5", BlockId));
                        command.Parameters.Add(new SqlParameter("@6", ShapeId));
                        command.Parameters.Add(new SqlParameter("@7", now));
                        command.Parameters.Add(new SqlParameter("@8", now));
                        break;
                    }
                case "Update":
                    {
                        command.Parameters.Add(new SqlParameter("@0", Id));
                        command.Parameters.Add(new SqlParameter("@1", RouteId));
                        command.Parameters.Add(new SqlParameter("@2", ServiceId));
                        command.Parameters.Add(new SqlParameter("@3", Headsign));
                        command.Parameters.Add(new SqlParameter("@4", DirectionId));
                        command.Parameters.Add(new SqlParameter("@5", BlockId));
                        command.Parameters.Add(new SqlParameter("@6", ShapeId));
                        command.Parameters.Add(new SqlParameter("@7", now));
                        break;
                    }
            }
        }

        public override void SetMySqlParameters(DbCommand command, string type)
        {
            var now = DateTime.UtcNow;

            switch (type)
            {
                case "Select":
                    {
                        command.Parameters.Add(new MySqlParameter("@0", Id));
                        break;
                    }
                case "Insert":
                    {
                        command.Parameters.Add(new MySqlParameter("@0", Id));
                        command.Parameters.Add(new MySqlParameter("@1", RouteId));
                        command.Parameters.Add(new MySqlParameter("@2", ServiceId));
                        command.Parameters.Add(new MySqlParameter("@3", Headsign));
                        command.Parameters.Add(new MySqlParameter("@4", DirectionId));
                        command.Parameters.Add(new MySqlParameter("@5", BlockId));
                        command.Parameters.Add(new MySqlParameter("@6", ShapeId));
                        command.Parameters.Add(new MySqlParameter("@7", now));
                        command.Parameters.Add(new MySqlParameter("@8", now));
                        break;
                    }
                case "Update":
                    {
                        command.Parameters.Add(new MySqlParameter("@0", Id));
                        command.Parameters.Add(new MySqlParameter("@1", RouteId));
                        command.Parameters.Add(new MySqlParameter("@2", ServiceId));
                        command.Parameters.Add(new MySqlParameter("@3", Headsign));
                        command.Parameters.Add(new MySqlParameter("@4", DirectionId));
                        command.Parameters.Add(new MySqlParameter("@5", BlockId));
                        command.Parameters.Add(new MySqlParameter("@6", ShapeId));
                        command.Parameters.Add(new MySqlParameter("@7", now));
                        break;
                    }
            }
        }

        public override bool Compare(DbDataReader reader, DbModel model)
        {
            var row = new Trip();
            var trip = (Trip)model;
            reader.Read();
            row.Id = reader.GetString(0).TrimEnd();
            row.RouteId = reader.GetString(1).TrimEnd();
            row.ServiceId = reader.GetString(2).TrimEnd();
            row.Headsign = reader.GetString(3).TrimEnd();
            row.DirectionId = reader.GetInt16(4);
            row.BlockId = reader.GetString(5).TrimEnd();
            row.ShapeId = reader.GetString(6).TrimEnd();

            if (trip.RouteId != row.RouteId || trip.ServiceId != row.ServiceId || trip.Headsign != row.Headsign || trip.DirectionId != row.DirectionId
                || trip.BlockId != row.BlockId || trip.ShapeId != row.ShapeId)
                return true;

            return false;
        }
    }

    public sealed class TripMap : CsvClassMap<Trip>
    {
        public TripMap()
        {
            Map(m => m.BlockId).Index(0);
            Map(m => m.RouteId).Index(1);
            Map(m => m.DirectionId).Index(2);
            Map(m => m.Headsign).Index(3);
            Map(m => m.ShapeId).Index(4);
            Map(m => m.ServiceId).Index(5);
            Map(m => m.Id).Index(6);
        }
    }
}
