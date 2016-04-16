using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetAroundAuckland.Models
{
    public abstract class DbModel
    {
        public DateTime CreatedTime { get; set; }
        public DateTime LastUpdatedTime { get; set; }

        public abstract void SetSqlParameters(SqlCommand command, string type);
        public abstract bool Compare(SqlDataReader reader, DbModel model);
    }
}
