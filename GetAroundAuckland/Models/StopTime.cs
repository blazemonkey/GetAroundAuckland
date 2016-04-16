using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetAroundAuckland.Models
{
    public class StopTime
    {
        public string TripId { get; set; }
        public string ArrivalTime { get; set; }
        public string DepartureTime { get; set; }
        public string StopId { get; set; }
        public int StopSequence { get; set; }
        public string StopHeadsign { get; set; }
        public int? PickupType { get; set; }
        public int? DropOffType { get; set; }
        public int? ShapeDistance { get; set; }
    }

    public sealed class StopTimeMap : CsvClassMap<StopTime>
    {
        public StopTimeMap()
        {
            Map(m => m.TripId).Index(0);
            Map(m => m.ArrivalTime).Index(1);
            Map(m => m.DepartureTime).Index(2);
            Map(m => m.StopId).Index(3);
            Map(m => m.StopSequence).Index(4);
            Map(m => m.StopHeadsign).Index(5);
            Map(m => m.PickupType).Index(6);
            Map(m => m.DropOffType).Index(7);
            Map(m => m.ShapeDistance).Index(8);
        }
    }
}
