using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetAroundAuckland.Services.CsvService
{
    public interface ICsvService
    {
        List<T> Parse<T, TMap>(string path) where T : new() where TMap : CsvClassMap;
    }
}
