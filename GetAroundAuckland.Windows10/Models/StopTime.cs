using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetAroundAuckland.Windows10.Models
{
    public class StopTime
    {
        [JsonProperty(PropertyName = "trip_id")]
        public string TripId { get; set; }
        [JsonProperty(PropertyName = "arrival_time")]
        public string ArrivalTime { get; set; }
        [JsonProperty(PropertyName = "departure_time")]
        public string DepartureTime { get; set; }
        [JsonProperty(PropertyName = "stop_id")]
        public string StopId { get; set; }
        [JsonProperty(PropertyName = "stop_sequence")]
        public int StopSequence { get; set; }
        [JsonProperty(PropertyName = "stop_headsign")]
        public string StopHeadsign { get; set; }
        [JsonProperty(PropertyName = "pickup_type")]
        public int? PickupType { get; set; }
        [JsonProperty(PropertyName = "drop_off_type")]
        public int? DropOffType { get; set; }
        [JsonProperty(PropertyName = "shape_dist_traveled")]
        public int? ShapeDistance { get; set; }
    }

    public class StopTimeResponse
    {
        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }
        [JsonProperty(PropertyName = "response")]
        public List<StopTime> Response { get; set; }
        [JsonProperty(PropertyName = "error")]
        public string Error { get; set; }
    }
}
