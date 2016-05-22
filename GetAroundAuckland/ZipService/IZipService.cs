using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetAroundAuckland.Services.ZipService
{
    public interface IZipService
    {
        bool Unzip(string zipPath, string extractPath);
    }
}
