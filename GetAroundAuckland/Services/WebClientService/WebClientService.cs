using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GetAroundAuckland.Services.WebClientService
{
    public class WebClientService : IWebClientService
    {
        public bool DownloadFile(string url, string filePath)
        {
            var stopWatch = new Stopwatch();

            try
            {
                using (var client = new WebClient())
                {                    
                    Logger.Info(string.Format("{0}: {1}", "Started Downloading", url));
                    stopWatch.Start();
                    client.DownloadFile(url, filePath);
                    stopWatch.Stop();
                    Logger.Info("Finished Downloading.");
                    Logger.Info("Time Taken (ms): " + stopWatch.ElapsedMilliseconds);
                }

                return true;
            }
            catch (Exception e)
            {
                Logger.Error(e);
                stopWatch.Stop();
                Logger.Info("Finished Downloading.");
                Logger.Info("Time Taken (ms): " + stopWatch.ElapsedMilliseconds);
                return false;
            }
        }
    }
}
