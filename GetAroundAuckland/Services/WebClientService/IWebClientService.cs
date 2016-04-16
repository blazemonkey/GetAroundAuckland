using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetAroundAuckland.Services.WebClientService
{
    public interface IWebClientService
    {
        bool DownloadFile(string url, string filePath);
    }
}
