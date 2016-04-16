using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using System.Diagnostics;
using System.IO;

namespace GetAroundAuckland.Services.ZipService
{
    public class ZipService : IZipService
    {
        public bool Unzip(string zipPath, string extractPath)
        {
            var stopWatch = new Stopwatch();

            try
            {
                Logger.Info("Started Extracting");
                stopWatch.Start();
                ZipFile.ExtractToDirectory(zipPath, extractPath);
                stopWatch.Stop();
                Logger.Info("Finished Extracting.");
                Logger.Info("Time Taken (ms): " + stopWatch.ElapsedMilliseconds);
                return true;
            }
            catch (Exception e)
            {
                Logger.Error(e);
                stopWatch.Stop();
                Logger.Info("Finished Extracting.");
                Logger.Info("Time Taken (ms): " + stopWatch.ElapsedMilliseconds);
                return false;
            }
            finally
            {
                //File.Delete(zipPath);
            }
        }
    }
}
