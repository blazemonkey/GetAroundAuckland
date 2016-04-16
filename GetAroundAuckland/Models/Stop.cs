using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetAroundAuckland.Models
{
    public class Stop
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string ZoneId { get; set; }
        public int Code { get; set; }
        public int LocationType { get; set; }
        public string ParentStation { get; set; }
    }

    public sealed class StopMap : CsvClassMap<Stop>
    {
        public StopMap()
        {
            Map(m => m.Latitude).Index(0);
            Map(m => m.ZoneId).Index(1);
            Map(m => m.Longitude).Index(2);
            Map(m => m.Id).Index(3);
            Map(m => m.ParentStation).Index(4);
            Map(m => m.Description).Index(5);
            Map(m => m.Name).Index(6);
            Map(m => m.LocationType).Index(7);
            Map(m => m.Code).Index(8);
        }
    }
}
