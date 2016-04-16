using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace GetAroundAuckland.Models
{   
    public class Agency : DbModel
    {
        public static string SelectSql = "SELECT * FROM agencies WHERE Id = @0";
        public static string InsertSql = "INSERT INTO agencies(Id, Name, Url, TimeZone, Lang, Phone, CreatedTime, LastUpdatedTime) VALUES (@0, @1, @2, @3, @4, @5, @6, @7)";
        public static string UpdateSql = "UPDATE agencies SET Name = @1, Url = @2, TimeZone = @3, Lang = @4, Phone = @5, LastUpdatedTime = @6  WHERE Id = @0";

        public string Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string TimeZone { get; set; }
        public string Lang { get; set; }
        public string Phone { get; set; }

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
                        command.Parameters.Add(new SqlParameter("@2", Url));
                        command.Parameters.Add(new SqlParameter("@3", TimeZone));
                        command.Parameters.Add(new SqlParameter("@4", Lang));
                        command.Parameters.Add(new SqlParameter("@5", Phone));
                        command.Parameters.Add(new SqlParameter("@6", now));
                        command.Parameters.Add(new SqlParameter("@7", now));
                        break;
                    }
                case "Update":
                    {
                        command.Parameters.Add(new SqlParameter("@0", Id));
                        command.Parameters.Add(new SqlParameter("@1", Name));
                        command.Parameters.Add(new SqlParameter("@2", Url));
                        command.Parameters.Add(new SqlParameter("@3", TimeZone));
                        command.Parameters.Add(new SqlParameter("@4", Lang));
                        command.Parameters.Add(new SqlParameter("@5", Phone));
                        command.Parameters.Add(new SqlParameter("@6", now));
                        break;
                    }
            }
        }

        public override bool Compare(SqlDataReader reader, DbModel model)
        {
            var row = new Agency();
            var agency = (Agency)model;
            reader.Read();
            row.Id = reader.GetString(0).TrimEnd();
            row.Name = reader.GetString(1).TrimEnd();
            row.Url = reader.GetString(2).TrimEnd();
            row.TimeZone = reader.GetString(3).TrimEnd();
            row.Lang = reader.GetString(4).TrimEnd();
            row.Phone = reader.GetString(5).TrimEnd();

            if (agency.Name != row.Name || agency.Url != row.Url || agency.TimeZone != row.TimeZone || agency.Lang != row.Lang || agency.Phone != row.Phone)
                return true;

            return false;
        }
    }

    public sealed class AgencyMap : CsvClassMap<Agency>
    {
        public AgencyMap()
        {
            Map(m => m.Phone).Index(0);
            Map(m => m.Url).Index(1);
            Map(m => m.Id).Index(2);
            Map(m => m.Name).Index(3);            
            Map(m => m.TimeZone).Index(4);
            Map(m => m.Lang).Index(5);
        }
    }
}
