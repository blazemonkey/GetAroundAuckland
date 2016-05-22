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
    public class StopTime : DbModel
    {
        public static string SelectSql = "SELECT * FROM stop_times WHERE TripId = @0 AND StopSequence = @1";
        public static string InsertSql = "INSERT INTO stop_times(TripId, ArrivalTime, DepartureTime, StopId, StopSequence, StopHeadsign, PickupType, DropoffType, " + 
                                         "ShapeDistTravelled, CreatedTime, LastUpdatedTime) VALUES (@0, @1, @2, @3, @4, @5, @6, @7, @8, @9, @10)";
        public static string UpdateSql = "UPDATE stop_times SET ArrivalTime = @2, DepartureTime = @3, StopId = @4, StopSequence = @5, StopHeadsign = @6, " + 
                                         "PickupType = @7, DropoffType = @8, LastUpdatedTime = @9  WHERE TripId = @0 AND StopId = @1";

        public string TripId { get; set; }
        public string ArrivalTime { get; set; }
        public string DepartureTime { get; set; }
        public string StopId { get; set; }
        public int StopSequence { get; set; }
        public string StopHeadsign { get; set; }
        public int? PickupType { get; set; }
        public int? DropOffType { get; set; }
        public int? ShapeDistance { get; set; }

        public override void SetSqlParameters(DbCommand command, string type)
        {
            var now = DateTime.UtcNow;

            switch (type)
            {
                case "Select":
                    {
                        command.Parameters.Add(new SqlParameter("@0", TripId));
                        command.Parameters.Add(new SqlParameter("@1", StopSequence));
                        break;
                    }
                case "Insert":
                    {
                        command.Parameters.Add(new SqlParameter("@0", TripId));
                        command.Parameters.Add(new SqlParameter("@1", ArrivalTime));
                        command.Parameters.Add(new SqlParameter("@2", DepartureTime));
                        command.Parameters.Add(new SqlParameter("@3", StopId));
                        command.Parameters.Add(new SqlParameter("@4", StopSequence));
                        command.Parameters.Add(new SqlParameter("@5", StopHeadsign));
                        if (PickupType == null)
                            command.Parameters.Add(new SqlParameter("@6", DBNull.Value));
                        else
                            command.Parameters.Add(new SqlParameter("@6", PickupType.Value));
                        if (DropOffType == null)
                            command.Parameters.Add(new SqlParameter("@7", DBNull.Value));
                        else
                            command.Parameters.Add(new SqlParameter("@7", DropOffType.Value));
                        if (ShapeDistance == null)
                            command.Parameters.Add(new SqlParameter("@8", DBNull.Value));
                        else
                            command.Parameters.Add(new SqlParameter("@8", ShapeDistance.Value));
                        command.Parameters.Add(new SqlParameter("@9", now));
                        command.Parameters.Add(new SqlParameter("@10", now));
                        break;
                    }
                case "Update":
                    {
                        command.Parameters.Add(new SqlParameter("@0", TripId));
                        command.Parameters.Add(new SqlParameter("@1", ArrivalTime));
                        command.Parameters.Add(new SqlParameter("@2", DepartureTime));
                        command.Parameters.Add(new SqlParameter("@3", StopId));
                        command.Parameters.Add(new SqlParameter("@4", StopSequence));
                        command.Parameters.Add(new SqlParameter("@5", StopHeadsign));
                        if (PickupType == null)
                            command.Parameters.Add(new SqlParameter("@6", DBNull.Value));
                        else
                            command.Parameters.Add(new SqlParameter("@6", PickupType.Value));
                        if (DropOffType == null)
                            command.Parameters.Add(new SqlParameter("@7", DBNull.Value));
                        else
                            command.Parameters.Add(new SqlParameter("@7", DropOffType.Value));
                        if (ShapeDistance == null)
                            command.Parameters.Add(new SqlParameter("@8", DBNull.Value));
                        else
                            command.Parameters.Add(new SqlParameter("@8", ShapeDistance.Value));
                        command.Parameters.Add(new SqlParameter("@9", now));
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
                        command.Parameters.Add(new MySqlParameter("@0", TripId));
                        command.Parameters.Add(new MySqlParameter("@1", StopSequence));
                        break;
                    }
                case "Insert":
                    {
                        command.Parameters.Add(new MySqlParameter("@0", TripId));
                        command.Parameters.Add(new MySqlParameter("@1", ArrivalTime));
                        command.Parameters.Add(new MySqlParameter("@2", DepartureTime));
                        command.Parameters.Add(new MySqlParameter("@3", StopId));
                        command.Parameters.Add(new MySqlParameter("@4", StopSequence));
                        command.Parameters.Add(new MySqlParameter("@5", StopHeadsign));
                        if (PickupType == null)
                            command.Parameters.Add(new MySqlParameter("@6", DBNull.Value));
                        else
                            command.Parameters.Add(new MySqlParameter("@6", PickupType.Value));
                        if (DropOffType == null)
                            command.Parameters.Add(new MySqlParameter("@7", DBNull.Value));
                        else
                            command.Parameters.Add(new MySqlParameter("@7", DropOffType.Value));
                        if (ShapeDistance == null)
                            command.Parameters.Add(new MySqlParameter("@8", DBNull.Value));
                        else
                            command.Parameters.Add(new MySqlParameter("@8", ShapeDistance.Value));
                        command.Parameters.Add(new MySqlParameter("@9", now));
                        command.Parameters.Add(new MySqlParameter("@10", now));
                        break;
                    }
                case "Update":
                    {
                        command.Parameters.Add(new MySqlParameter("@0", TripId));
                        command.Parameters.Add(new MySqlParameter("@1", ArrivalTime));
                        command.Parameters.Add(new MySqlParameter("@2", DepartureTime));
                        command.Parameters.Add(new MySqlParameter("@3", StopId));
                        command.Parameters.Add(new MySqlParameter("@4", StopSequence));
                        command.Parameters.Add(new MySqlParameter("@5", StopHeadsign));
                        if (PickupType == null)
                            command.Parameters.Add(new MySqlParameter("@6", DBNull.Value));
                        else
                            command.Parameters.Add(new MySqlParameter("@6", PickupType.Value));
                        if (DropOffType == null)
                            command.Parameters.Add(new MySqlParameter("@7", DBNull.Value));
                        else
                            command.Parameters.Add(new MySqlParameter("@7", DropOffType.Value));
                        if (ShapeDistance == null)
                            command.Parameters.Add(new MySqlParameter("@8", DBNull.Value));
                        else
                            command.Parameters.Add(new MySqlParameter("@8", ShapeDistance.Value));
                        command.Parameters.Add(new MySqlParameter("@9", now));
                        break;
                    }
            }
        }

        public override bool Compare(DbDataReader reader, DbModel model)
        {
            var row = new StopTime();
            var stopTime = (StopTime)model;
            reader.Read();
            row.TripId = reader.GetString(0).TrimEnd();
            row.ArrivalTime = reader.GetString(1).TrimEnd();
            row.DepartureTime = reader.GetString(2).TrimEnd();
            row.StopId = reader.GetString(3).TrimEnd();
            row.StopSequence = reader.GetInt32(4);
            row.StopHeadsign = reader.GetString(5).TrimEnd();
            if (reader.IsDBNull(6))
                row.PickupType = null;
            else
                row.PickupType = reader.GetInt32(6);
            if (reader.IsDBNull(7))
                row.DropOffType = null;
            else
                row.DropOffType = reader.GetInt32(7);
            if (reader.IsDBNull(8))
                row.ShapeDistance = null;
            else
                row.ShapeDistance = reader.GetInt32(4);

            if (stopTime.TripId != row.TripId || stopTime.ArrivalTime != row.ArrivalTime || stopTime.DepartureTime != row.DepartureTime || stopTime.StopId != row.StopId || stopTime.StopSequence != row.StopSequence ||
                stopTime.PickupType != row.PickupType || stopTime.DropOffType != row.DropOffType || stopTime.ShapeDistance != row.ShapeDistance)
                return true;

            return false;
        }
    }

    public sealed class StopTimeMap : CsvClassMap<StopTime>
    {
        public StopTimeMap()
        {
            Map(m => m.TripId).Index(0);
            Map(m => m.ArrivalTime).Index(1);
            Map(m => m.DepartureTime).Index(2);
            Map(m => m.StopId).Index(3);
            Map(m => m.StopSequence).Index(4);
            Map(m => m.StopHeadsign).Index(5);
            Map(m => m.PickupType).Index(6);
            Map(m => m.DropOffType).Index(7);
            Map(m => m.ShapeDistance).Index(8);
        }
    }
}
