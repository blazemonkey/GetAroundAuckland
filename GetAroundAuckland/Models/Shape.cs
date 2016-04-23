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
    public class Shape : DbModel
    {
        public static string SelectSql = "SELECT * FROM shapes WHERE Id = @0 AND Sequence = @1";
        public static string InsertSql = "INSERT INTO shapes(Id, Latitude, Longitude, Sequence, Distance, CreatedTime, LastUpdatedTime) VALUES (@0, @1, @2, @3, @4, @5, @6)";
        public static string UpdateSql = "UPDATE shapes SET Latitude = @2, Longitude = @3, Distance = @4, LastUpdatedTime = @5  WHERE Id = @0 AND Sequence = @1";

        public string Id { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public int Sequence { get; set; }
        public int? Distance { get; set; }

        public override void SetSqlParameters(DbCommand command, string type)
        {
            var now = DateTime.UtcNow;

            switch (type)
            {
                case "Select":
                    {
                        command.Parameters.Add(new SqlParameter("@0", Id));
                        command.Parameters.Add(new SqlParameter("@1", Sequence));
                        break;
                    }
                case "Insert":
                    {
                        command.Parameters.Add(new SqlParameter("@0", Id));
                        command.Parameters.Add(new SqlParameter("@1", Latitude));
                        command.Parameters.Add(new SqlParameter("@2", Longitude));
                        command.Parameters.Add(new SqlParameter("@3", Sequence));
                        if (Distance == null)
                            command.Parameters.Add(new SqlParameter("@4", DBNull.Value));
                        else
                            command.Parameters.Add(new SqlParameter("@4", Distance.Value));
                        command.Parameters.Add(new SqlParameter("@5", now));
                        command.Parameters.Add(new SqlParameter("@6", now));
                        break;
                    }
                case "Update":
                    {
                        command.Parameters.Add(new SqlParameter("@0", Id));
                        command.Parameters.Add(new SqlParameter("@1", Sequence));
                        command.Parameters.Add(new SqlParameter("@2", Latitude));
                        command.Parameters.Add(new SqlParameter("@3", Longitude));                        
                        if (Distance == null)
                            command.Parameters.Add(new SqlParameter("@4", DBNull.Value));
                        else
                            command.Parameters.Add(new SqlParameter("@4", Distance.Value));
                        command.Parameters.Add(new SqlParameter("@5", now));
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
                        command.Parameters.Add(new MySqlParameter("@1", Sequence));
                        break;
                    }
                case "Insert":
                    {
                        command.Parameters.Add(new MySqlParameter("@0", Id));
                        command.Parameters.Add(new MySqlParameter("@1", Latitude));
                        command.Parameters.Add(new MySqlParameter("@2", Longitude));
                        command.Parameters.Add(new MySqlParameter("@3", Sequence));
                        if (Distance == null)
                            command.Parameters.Add(new MySqlParameter("@4", DBNull.Value));
                        else
                            command.Parameters.Add(new MySqlParameter("@4", Distance.Value));
                        command.Parameters.Add(new MySqlParameter("@5", now));
                        command.Parameters.Add(new MySqlParameter("@6", now));
                        break;
                    }
                case "Update":
                    {
                        command.Parameters.Add(new MySqlParameter("@0", Id));
                        command.Parameters.Add(new MySqlParameter("@1", Sequence));
                        command.Parameters.Add(new MySqlParameter("@2", Latitude));
                        command.Parameters.Add(new MySqlParameter("@3", Longitude));
                        if (Distance == null)
                            command.Parameters.Add(new MySqlParameter("@4", DBNull.Value));
                        else
                            command.Parameters.Add(new MySqlParameter("@4", Distance.Value));
                        command.Parameters.Add(new MySqlParameter("@5", now));
                        break;
                    }
            }
        }

        public override bool Compare(DbDataReader reader, DbModel model)
        {
            var row = new Shape();
            var shape = (Shape)model;
            reader.Read();
            row.Id = reader.GetString(0).TrimEnd();
            row.Latitude = reader.GetDecimal(1);
            row.Longitude = reader.GetDecimal(2);
            row.Sequence = reader.GetInt32(3);
            if (reader.IsDBNull(4))
                row.Distance = null;
            else
                row.Distance = reader.GetInt32(4);

            if (shape.Latitude != row.Latitude || shape.Longitude != row.Longitude || shape.Sequence != row.Sequence || shape.Distance != row.Distance)
                return true;

            return false;
        }
    }

    public sealed class ShapeMap : CsvClassMap<Shape>
    {
        public ShapeMap()
        {
            Map(m => m.Id).Index(0);
            Map(m => m.Latitude).Index(1);
            Map(m => m.Longitude).Index(2);
            Map(m => m.Sequence).Index(3);
            Map(m => m.Distance).Index(4);
        }
    }
}
