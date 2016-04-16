using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetAroundAuckland.Models
{
    public class Route
    {
        public string Id { get; set; }
        public string AgencyId { get; set; }
        public string ShortName { get; set; }
        public string LongName { get; set; }
        public string Description { get; set; }
        public int Type { get; set; }
        public string Url { get; set; }
        public string Color { get; set; }
        public string TextColor { get; set; }
    }

    public sealed class RouteMap : CsvClassMap<Route>
    {
        public RouteMap()
        {
            Map(m => m.LongName).Index(0);
            Map(m => m.Type).Index(1);
            Map(m => m.TextColor).Index(2);
            Map(m => m.AgencyId).Index(3);
            Map(m => m.Id).Index(4);
            Map(m => m.Color).Index(5);
            Map(m => m.ShortName).Index(6);
        }
    }
}
