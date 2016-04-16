using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetAroundAuckland.Services.CsvService
{
    public class CsvService : ICsvService
    {
        public List<T> Parse<T, TMap>(string path) where T : new()
                                                   where TMap : CsvClassMap
        {
            var stopWatch = new Stopwatch();

            try
            {
                var items = new List<T>();

                using (var fileStream = File.OpenRead(path))
                {
                    using (var streamReader = new StreamReader(fileStream))
                    {
                        
                        Logger.Info(string.Format("{0}: {1}", "Started Parsing: ", path));
                        stopWatch.Start();
                        var csv = new CsvReader(streamReader);
                        csv.Configuration.RegisterClassMap<TMap>();
                        items = csv.GetRecords<T>().ToList();
                        stopWatch.Stop();
                        Logger.Info("Finished Parsing.");
                        Logger.Info("Time Taken (ms): " + stopWatch.ElapsedMilliseconds);
                    }
                }

                return items;
            }
            catch (Exception e)
            {
                Logger.Error(e);
                stopWatch.Stop();
                Logger.Info("Finished Parsing.");
                Logger.Info("Time Taken (ms): " + stopWatch.ElapsedMilliseconds);
                return null;
            }
            finally
            {
                //File.Delete(path);
            }        
        }
    }
}
