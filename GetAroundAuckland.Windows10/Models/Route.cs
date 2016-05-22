using Newtonsoft.Json;
using SQLite.Net.Attributes;
using System.Collections.Generic;

namespace GetAroundAuckland.Windows10.Models
{
    public class Route
    {
        [PrimaryKey]
        [JsonProperty(PropertyName = "route_id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "agency_id")]
        public string AgencyId { get; set; }
        [JsonProperty(PropertyName = "route_short_name")]
        public string ShortName { get; set; }
        [JsonProperty(PropertyName = "route_long_name")]
        public string LongName { get; set; }
        [JsonProperty(PropertyName = "route_type")]
        public byte Type { get; set; }
        [JsonProperty(PropertyName = "route_color")]
        public string Color { get; set; }
        [JsonProperty(PropertyName = "route_text_color")]
        public string TextColor { get; set; }
        public bool IsLatest { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1} - {2}", AgencyId, ShortName, LongName);
        }
    }

    public class RouteResponse
    {
        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }
        [JsonProperty(PropertyName = "response")]
        public List<Route> Response { get; set; }
        [JsonProperty(PropertyName = "error")]
        public string Error { get; set; }
    }
}
