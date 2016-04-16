using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetAroundAuckland.Models
{
    public class Trip
    {
        public string Id { get; set; }
        public string RouteId { get; set; }
        public string ServiceId { get; set; }
        public string Headsign { get; set; }
        public int DirectionId { get; set; }
        public string BlockId { get; set; }
        public string ShapeId { get; set; }
    }

    public sealed class TripMap : CsvClassMap<Trip>
    {
        public TripMap()
        {
            Map(m => m.BlockId).Index(0);
            Map(m => m.RouteId).Index(1);
            Map(m => m.DirectionId).Index(2);
            Map(m => m.Headsign).Index(3);
            Map(m => m.ShapeId).Index(4);
            Map(m => m.ServiceId).Index(5);
            Map(m => m.Id).Index(6);
        }
    }
}
