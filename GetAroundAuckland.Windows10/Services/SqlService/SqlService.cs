using GetAroundAuckland.Windows10.Models;
using Microsoft.Practices.Unity;
using MySql.Data.MySqlClient;
using Services.FileReaderService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetAroundAuckland.Windows10.Services.SqlService
{
    public class SqlService : ISqlService
    {
        public string ConnectionString { get; set; }

        [Dependency]
        public IFileReaderService FileReaderService { get; set; }

        public SqlService()
        {
            //ConnectionString = FileReaderService.ReadFile("connString.txt", "Configurations").Result;
            ConnectionString = "Server=localhost;Database=auckland_transport;SslMode=None;Uid=gary;Pwd=serpentS7&amp;";
        }

        public Task<List<Route>> GetRoutes()
        {
            try
            {
                using (var conn = new MySqlConnection(ConnectionString))
                {
                    conn.Open();
                    conn.Close();
                }
            }
            catch (Exception e)
            {

            }

            return null;
        }
    }
}
