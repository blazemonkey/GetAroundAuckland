using Newtonsoft.Json;
using SQLite.Net.Attributes;
using System.Collections.Generic;

namespace GetAroundAuckland.Windows10.Models
{
    public class Agency
    {
        [PrimaryKey]
        [JsonProperty(PropertyName = "agency_id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "agency_name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "agency_url")]
        public string Url { get; set; }
        [JsonProperty(PropertyName = "agency_timezone")]
        public string TimeZone { get; set; }
        [JsonProperty(PropertyName = "agency_lang")]
        public string Lang { get; set; }
        [JsonProperty(PropertyName = "agency_phone")]
        public string Phone { get; set; }

        public static Agency CreateDefault()
        {
            return new Agency { Id = "All", Name = "all agencies" };
        }
    }

    public class AgencyResponse
    {
        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }
        [JsonProperty(PropertyName = "response")]
        public List<Agency> Response { get; set; }
        [JsonProperty(PropertyName = "error")]
        public string Error { get; set; }
    }
}
