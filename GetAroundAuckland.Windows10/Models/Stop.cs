using Newtonsoft.Json;
using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetAroundAuckland.Windows10.Models
{
    public class Stop
    {
        [Ignore]
        public static List<Stop> ListOfStops { get; set; }

        [PrimaryKey]
        [JsonProperty(PropertyName = "stop_id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "stop_name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "stop_desc")]
        public string Description { get; set; }
        [JsonProperty(PropertyName = "stop_lat")]
        public decimal Latitude { get; set; }
        [JsonProperty(PropertyName = "stop_lon")]
        public decimal Longitude { get; set; }
        [JsonProperty(PropertyName = "zone_id")]
        public string ZoneId { get; set; }
        [JsonProperty(PropertyName = "stop_code")]
        public int Code { get; set; }
        [JsonProperty(PropertyName = "location_type")]
        public byte LocationType { get; set; }
        [JsonProperty(PropertyName = "parent_station")]
        public string ParentStation { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1}", Id, Name);
        }        
    }

    public class StopResponse
    {
        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }
        [JsonProperty(PropertyName = "response")]
        public List<Stop> Response { get; set; }
        [JsonProperty(PropertyName = "error")]
        public string Error { get; set; }
    }
}
