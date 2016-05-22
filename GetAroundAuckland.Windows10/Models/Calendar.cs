using Newtonsoft.Json;
using SQLite.Net.Attributes;
using System.Collections.Generic;

namespace GetAroundAuckland.Windows10.Models
{
    public class Calendar
    {
        [PrimaryKey]
        [JsonProperty(PropertyName = "service_id")]
        public string ServiceId { get; set; }
        [JsonProperty(PropertyName = "start_date")]
        public string StartDate { get; set; }
        [JsonProperty(PropertyName = "end_date")]
        public string EndDate { get; set; }
        [JsonProperty(PropertyName = "monday")]
        public bool Monday { get; set; }
        [JsonProperty(PropertyName = "tuesday")]
        public bool Tuesday { get; set; }
        [JsonProperty(PropertyName = "wednesday")]
        public bool Wednesday { get; set; }
        [JsonProperty(PropertyName = "thursday")]
        public bool Thursday { get; set; }
        [JsonProperty(PropertyName = "friday")]
        public bool Friday { get; set; }
        [JsonProperty(PropertyName = "saturday")]
        public bool Saturday { get; set; }
        [JsonProperty(PropertyName = "sunday")]
        public bool Sunday { get; set; }
    }

    public class CalendarResponse
    {
        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }
        [JsonProperty(PropertyName = "response")]
        public List<Calendar> Response { get; set; }
        [JsonProperty(PropertyName = "error")]
        public string Error { get; set; }
    }
}
