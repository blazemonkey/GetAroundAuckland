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
    public class Calendar : DbModel
    {
        public static string SelectSql = "SELECT * FROM calendars WHERE ServiceId = @0";
        public static string InsertSql = "INSERT INTO calendars(ServiceId, StartDate, EndDate, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday, CreatedTime, LastUpdatedTime) " + 
                                        "VALUES (@0, @1, @2, @3, @4, @5, @6, @7, @8, @9, @10, @11)";
        public static string UpdateSql = "UPDATE calendars SET StartDate = @1, EndDate = @2, Monday = @3, Tuesday = @4, Wednesday = @5, " + 
                                         "Thursday = @6, Friday = @7, Saturday = @8, Sunday = @9, LastUpdatedTime = @10  WHERE ServiceId = @0";

        public string ServiceId { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        public bool Sunday { get; set; }

        public override void SetSqlParameters(DbCommand command, string type)
        {
            var now = DateTime.UtcNow;

            switch (type)
            {
                case "Select":
                    {
                        command.Parameters.Add(new SqlParameter("@0", ServiceId));
                        break;
                    }
                case "Insert":
                    {
                        command.Parameters.Add(new SqlParameter("@0", ServiceId));
                        command.Parameters.Add(new SqlParameter("@1", StartDate));
                        command.Parameters.Add(new SqlParameter("@2", EndDate));
                        command.Parameters.Add(new SqlParameter("@3", Monday));
                        command.Parameters.Add(new SqlParameter("@4", Tuesday));
                        command.Parameters.Add(new SqlParameter("@5", Wednesday));
                        command.Parameters.Add(new SqlParameter("@6", Thursday));
                        command.Parameters.Add(new SqlParameter("@7", Friday));
                        command.Parameters.Add(new SqlParameter("@8", Saturday));
                        command.Parameters.Add(new SqlParameter("@9", Sunday));
                        command.Parameters.Add(new SqlParameter("@10", now));
                        command.Parameters.Add(new SqlParameter("@11", now));
                        break;
                    }
                case "Update":
                    {
                        command.Parameters.Add(new SqlParameter("@0", ServiceId));
                        command.Parameters.Add(new SqlParameter("@1", StartDate));
                        command.Parameters.Add(new SqlParameter("@2", EndDate));
                        command.Parameters.Add(new SqlParameter("@3", Monday));
                        command.Parameters.Add(new SqlParameter("@4", Tuesday));
                        command.Parameters.Add(new SqlParameter("@5", Wednesday));
                        command.Parameters.Add(new SqlParameter("@6", Thursday));
                        command.Parameters.Add(new SqlParameter("@7", Friday));
                        command.Parameters.Add(new SqlParameter("@8", Saturday));
                        command.Parameters.Add(new SqlParameter("@9", Sunday));
                        command.Parameters.Add(new SqlParameter("@10", now));
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
                        command.Parameters.Add(new MySqlParameter("@0", ServiceId));
                        break;
                    }
                case "Insert":
                    {
                        command.Parameters.Add(new MySqlParameter("@0", ServiceId));
                        command.Parameters.Add(new MySqlParameter("@1", StartDate));
                        command.Parameters.Add(new MySqlParameter("@2", EndDate));
                        command.Parameters.Add(new MySqlParameter("@3", Monday));
                        command.Parameters.Add(new MySqlParameter("@4", Tuesday));
                        command.Parameters.Add(new MySqlParameter("@5", Wednesday));
                        command.Parameters.Add(new MySqlParameter("@6", Thursday));
                        command.Parameters.Add(new MySqlParameter("@7", Friday));
                        command.Parameters.Add(new MySqlParameter("@8", Saturday));
                        command.Parameters.Add(new MySqlParameter("@9", Sunday));
                        command.Parameters.Add(new MySqlParameter("@10", now));
                        command.Parameters.Add(new MySqlParameter("@11", now));
                        break;
                    }
                case "Update":
                    {
                        command.Parameters.Add(new MySqlParameter("@0", ServiceId));
                        command.Parameters.Add(new MySqlParameter("@1", StartDate));
                        command.Parameters.Add(new MySqlParameter("@2", EndDate));
                        command.Parameters.Add(new MySqlParameter("@3", Monday));
                        command.Parameters.Add(new MySqlParameter("@4", Tuesday));
                        command.Parameters.Add(new MySqlParameter("@5", Wednesday));
                        command.Parameters.Add(new MySqlParameter("@6", Thursday));
                        command.Parameters.Add(new MySqlParameter("@7", Friday));
                        command.Parameters.Add(new MySqlParameter("@8", Saturday));
                        command.Parameters.Add(new MySqlParameter("@9", Sunday));
                        command.Parameters.Add(new MySqlParameter("@10", now));
                        break;
                    }
            }
        }

        public override bool Compare(DbDataReader reader, DbModel model)
        {
            var row = new Calendar();
            var calendar = (Calendar)model;
            reader.Read();
            row.ServiceId = reader.GetString(0).TrimEnd();
            row.StartDate = reader.GetString(1).TrimEnd();
            row.EndDate = reader.GetString(2).TrimEnd();
            row.Monday = reader.GetBoolean(3);
            row.Tuesday = reader.GetBoolean(4);
            row.Wednesday = reader.GetBoolean(5);
            row.Thursday = reader.GetBoolean(6);
            row.Friday = reader.GetBoolean(7);
            row.Saturday = reader.GetBoolean(8);
            row.Sunday = reader.GetBoolean(9);

            if (calendar.StartDate != row.StartDate || calendar.EndDate != row.EndDate || calendar.Monday != row.Monday || calendar.Tuesday != row.Tuesday || calendar.Wednesday != row.Wednesday 
                || calendar.Thursday != row.Thursday || calendar.Friday != row.Friday || calendar.Saturday != row.Saturday || calendar.Sunday != row.Sunday)
                return true;

            return false;
        }
    }

    public sealed class CalendarMap : CsvClassMap<Calendar>
    {
        public CalendarMap()
        {
            Map(m => m.ServiceId).Index(0);
            Map(m => m.StartDate).Index(1);
            Map(m => m.EndDate).Index(2);
            Map(m => m.Monday).Index(3);
            Map(m => m.Tuesday).Index(4);
            Map(m => m.Wednesday).Index(5);
            Map(m => m.Thursday).Index(6);
            Map(m => m.Friday).Index(7);
            Map(m => m.Saturday).Index(8);
            Map(m => m.Sunday).Index(9);
        }
    }
    
}
