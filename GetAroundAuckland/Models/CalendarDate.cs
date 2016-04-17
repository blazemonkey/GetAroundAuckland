using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetAroundAuckland.Models
{
    public class CalendarDate : DbModel
    {
        public static string SelectSql = "SELECT * FROM calendar_dates WHERE ServiceId = @0";
        public static string InsertSql = "INSERT INTO calendar_dates(ServiceId, Date, ExceptionType, CreatedTime, LastUpdatedTime) VALUES (@0, @1, @2, @3, @4)";
        public static string UpdateSql = "UPDATE calendar_dates SET Date = @1, ExceptionType = @2, LastUpdatedTime = @3  WHERE ServiceId = @0";

        public string ServiceId { get; set; }
        public string Date { get; set; }
        public byte ExceptionType { get; set; }

        public override void SetSqlParameters(SqlCommand command, string type)
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
                        command.Parameters.Add(new SqlParameter("@1", Date));
                        command.Parameters.Add(new SqlParameter("@2", ExceptionType));
                        command.Parameters.Add(new SqlParameter("@3", now));
                        command.Parameters.Add(new SqlParameter("@4", now));
                        break;
                    }
                case "Update":
                    {
                        command.Parameters.Add(new SqlParameter("@0", ServiceId));
                        command.Parameters.Add(new SqlParameter("@1", Date));
                        command.Parameters.Add(new SqlParameter("@2", ExceptionType));
                        command.Parameters.Add(new SqlParameter("@3", now));
                        break;
                    }
            }
        }

        public override bool Compare(SqlDataReader reader, DbModel model)
        {
            var row = new CalendarDate();
            var calendarDate = (CalendarDate)model;
            reader.Read();
            row.ServiceId = reader.GetString(0).TrimEnd();
            row.Date = reader.GetString(1).TrimEnd();
            row.ExceptionType = reader.GetByte(2);

            if (calendarDate.Date != row.Date || calendarDate.ExceptionType != row.ExceptionType)
                return true;

            return false;
        }
    }

    public sealed class CalendarDateMap : CsvClassMap<CalendarDate>
    {
        public CalendarDateMap()
        {
            Map(m => m.ServiceId).Index(0);
            Map(m => m.Date).Index(1);
            Map(m => m.ExceptionType).Index(2);
        }
    }
}
