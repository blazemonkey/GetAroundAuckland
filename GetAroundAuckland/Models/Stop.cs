using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetAroundAuckland.Models
{
    public class Stop : DbModel
    {
        public static string SelectSql = "SELECT * FROM stops WHERE Id = @0";
        public static string InsertSql = "INSERT INTO stops(Id, Name, Description, Latitude, Longitude, ZoneId, Code, LocationType, ParentStation, CreatedTime, LastUpdatedTime) VALUES (@0, @1, @2, @3, @4, @5, @6, @7, @8, @9, @10)";
        public static string UpdateSql = "UPDATE shapes SET Name = @1, Description = @2, Latitude = @3, Longitude = @4, ZoneId = @5, Code = @6, LocationType = @7, " + 
                                         "ParentStation = @8, LastUpdatedTime = @9  WHERE Id = @0";

        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string ZoneId { get; set; }
        public int Code { get; set; }
        public byte LocationType { get; set; }
        public string ParentStation { get; set; }

        public override void SetSqlParameters(SqlCommand command, string type)
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
                        command.Parameters.Add(new SqlParameter("@1", Name));
                        command.Parameters.Add(new SqlParameter("@2", Description));
                        command.Parameters.Add(new SqlParameter("@3", Latitude));
                        command.Parameters.Add(new SqlParameter("@4", Longitude));
                        command.Parameters.Add(new SqlParameter("@5", ZoneId));
                        command.Parameters.Add(new SqlParameter("@6", Code));
                        command.Parameters.Add(new SqlParameter("@7", LocationType));
                        command.Parameters.Add(new SqlParameter("@8", ParentStation));
                        command.Parameters.Add(new SqlParameter("@9", now));
                        command.Parameters.Add(new SqlParameter("@10", now));
                        break;
                    }
                case "Update":
                    {
                        command.Parameters.Add(new SqlParameter("@0", Id));
                        command.Parameters.Add(new SqlParameter("@1", Name));
                        command.Parameters.Add(new SqlParameter("@2", Description));
                        command.Parameters.Add(new SqlParameter("@3", Latitude));
                        command.Parameters.Add(new SqlParameter("@4", Longitude));
                        command.Parameters.Add(new SqlParameter("@5", ZoneId));
                        command.Parameters.Add(new SqlParameter("@6", Code));
                        command.Parameters.Add(new SqlParameter("@7", LocationType));
                        command.Parameters.Add(new SqlParameter("@8", ParentStation));
                        command.Parameters.Add(new SqlParameter("@9", now));
                        break;
                    }
            }
        }

        public override bool Compare(SqlDataReader reader, DbModel model)
        {
            var row = new Stop();
            var stop = (Stop)model;
            reader.Read();
            row.Id = reader.GetString(0).TrimEnd();
            row.Name = reader.GetString(1).TrimEnd();
            row.Description = reader.GetString(2).TrimEnd();
            row.Latitude = reader.GetDecimal(3);
            row.Longitude = reader.GetDecimal(4);
            row.ZoneId = reader.GetString(5).TrimEnd();
            row.Code = reader.GetInt32(6);
            row.LocationType = reader.GetByte(7);
            row.ParentStation = reader.GetString(8).TrimEnd();

            if (stop.Name != row.Name || stop.Description != row.Description || stop.Latitude != row.Latitude || stop.Longitude != row.Longitude || stop.ZoneId != row.ZoneId ||
                stop.Code != row.Code || stop.LocationType != row.LocationType || stop.ParentStation != row.ParentStation)
                return true;

            return false;
        }
    }

    public sealed class StopMap : CsvClassMap<Stop>
    {
        public StopMap()
        {
            Map(m => m.Latitude).Index(0);
            Map(m => m.ZoneId).Index(1);
            Map(m => m.Longitude).Index(2);
            Map(m => m.Id).Index(3);
            Map(m => m.ParentStation).Index(4);
            Map(m => m.Description).Index(5);
            Map(m => m.Name).Index(6);
            Map(m => m.LocationType).Index(7);
            Map(m => m.Code).Index(8);
        }
    }
}
