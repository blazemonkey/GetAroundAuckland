using System;
using System.Data.Common;

namespace GetAroundAuckland.Models
{
    public abstract class DbModel
    {
        public DateTime CreatedTime { get; set; }
        public DateTime LastUpdatedTime { get; set; }

        public abstract void SetSqlParameters(DbCommand command, string type);
        public abstract void SetMySqlParameters(DbCommand command, string type);
        public abstract bool Compare(DbDataReader reader, DbModel model);
    }
}
