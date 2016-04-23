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
    public class Route : DbModel
    {
        public static string SelectSql = "SELECT * FROM routes WHERE Id = @0";
        public static string InsertSql = "INSERT INTO routes(Id, AgencyId, ShortName, LongName, Type, Color, TextColor, CreatedTime, LastUpdatedTime) " +
                                        "VALUES (@0, @1, @2, @3, @4, @5, @6, @7, @8)";
        public static string UpdateSql = "UPDATE routes SET AgencyId = @1, ShortName = @2, LongName = @3, Type = @4, " +
                                         "Color = @5, TextColor = @6, LastUpdatedTime = @7  WHERE ServiceId = @0";

        public string Id { get; set; }
        public string AgencyId { get; set; }
        public string ShortName { get; set; }
        public string LongName { get; set; }
        public byte Type { get; set; }
        public string Color { get; set; }
        public string TextColor { get; set; }

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
                        command.Parameters.Add(new SqlParameter("@1", AgencyId));
                        command.Parameters.Add(new SqlParameter("@2", ShortName));
                        command.Parameters.Add(new SqlParameter("@3", LongName));
                        command.Parameters.Add(new SqlParameter("@4", Type));
                        command.Parameters.Add(new SqlParameter("@5", Color));
                        command.Parameters.Add(new SqlParameter("@6", TextColor));
                        command.Parameters.Add(new SqlParameter("@7", now));
                        command.Parameters.Add(new SqlParameter("@8", now));
                        break;
                    }
                case "Update":
                    {
                        command.Parameters.Add(new SqlParameter("@0", Id));
                        command.Parameters.Add(new SqlParameter("@1", AgencyId));
                        command.Parameters.Add(new SqlParameter("@2", ShortName));
                        command.Parameters.Add(new SqlParameter("@3", LongName));
                        command.Parameters.Add(new SqlParameter("@4", Type));
                        command.Parameters.Add(new SqlParameter("@5", Color));
                        command.Parameters.Add(new SqlParameter("@6", TextColor));
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
                        command.Parameters.Add(new MySqlParameter("@1", AgencyId));
                        command.Parameters.Add(new MySqlParameter("@2", ShortName));
                        command.Parameters.Add(new MySqlParameter("@3", LongName));
                        command.Parameters.Add(new MySqlParameter("@4", Type));
                        command.Parameters.Add(new MySqlParameter("@5", Color));
                        command.Parameters.Add(new MySqlParameter("@6", TextColor));
                        command.Parameters.Add(new MySqlParameter("@7", now));
                        command.Parameters.Add(new MySqlParameter("@8", now));
                        break;
                    }
                case "Update":
                    {
                        command.Parameters.Add(new MySqlParameter("@0", Id));
                        command.Parameters.Add(new MySqlParameter("@1", AgencyId));
                        command.Parameters.Add(new MySqlParameter("@2", ShortName));
                        command.Parameters.Add(new MySqlParameter("@3", LongName));
                        command.Parameters.Add(new MySqlParameter("@4", Type));
                        command.Parameters.Add(new MySqlParameter("@5", Color));
                        command.Parameters.Add(new MySqlParameter("@6", TextColor));
                        command.Parameters.Add(new MySqlParameter("@7", now));
                        break;
                    }
            }
        }

        public override bool Compare(DbDataReader reader, DbModel model)
        {
            var row = new Route();
            var route = (Route)model;
            reader.Read();
            row.Id = reader.GetString(0).TrimEnd();
            row.LongName = reader.GetString(1).TrimEnd();
            row.ShortName = reader.GetString(2).TrimEnd();
            row.Type = reader.GetByte(3);
            row.TextColor = reader.GetString(4).TrimEnd();
            row.Color = reader.GetString(5).TrimEnd();
            row.AgencyId = reader.GetString(6).TrimEnd();

            if (route.AgencyId != row.AgencyId || route.ShortName != row.ShortName || route.LongName != row.LongName || route.Type != row.Type
                || route.Color != row.Color || route.TextColor != row.TextColor)
                return true;

            return false;
        }
    }

    public sealed class RouteMap : CsvClassMap<Route>
    {
        public RouteMap()
        {
            Map(m => m.LongName).Index(0);
            Map(m => m.Type).Index(1);
            Map(m => m.TextColor).Index(2);
            Map(m => m.AgencyId).Index(3);
            Map(m => m.Id).Index(4);
            Map(m => m.Color).Index(5);
            Map(m => m.ShortName).Index(6);
        }
    }
}
