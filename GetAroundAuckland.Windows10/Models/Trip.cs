using Newtonsoft.Json;
using System.Collections.Generic;

namespace GetAroundAuckland.Windows10.Models
{
    public class Trip
    {
        [JsonProperty(PropertyName = "trip_id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "route_id")]
        public string RouteId { get; set; }
        [JsonProperty(PropertyName = "service_id")]
        public string ServiceId { get; set; }
        [JsonProperty(PropertyName = "trip_headsign")]
        public string Headsign { get; set; }
        [JsonProperty(PropertyName = "direction_id")]
        public int DirectionId { get; set; }
        [JsonProperty(PropertyName = "block_id")]
        public string BlockId { get; set; }
        [JsonProperty(PropertyName = "shape_id")]
        public string ShapeId { get; set; }
        public string FirstArrivalTime { get; set; }
        public string LastDepartureTime { get; set; }
    }

    public class TripResponse
    {
        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }
        [JsonProperty(PropertyName = "response")]
        public List<Trip> Response { get; set; }
        [JsonProperty(PropertyName = "error")]
        public string Error { get; set; }
    }
}
