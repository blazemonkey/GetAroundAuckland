using Newtonsoft.Json;
using SQLite.Net.Attributes;
using System.Collections.Generic;

namespace GetAroundAuckland.Windows10.Models
{
    public class CalendarDate
    {
        [PrimaryKey]
        [JsonProperty(PropertyName = "service_id")]
        public string ServiceId { get; set; }
        [JsonProperty(PropertyName = "date")]
        public string Date { get; set; }
        [JsonProperty(PropertyName = "exception_type")]
        public byte ExceptionType { get; set; }
    }

    public class CalendarDateResponse
    {
        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }
        [JsonProperty(PropertyName = "response")]
        public List<CalendarDate> Response { get; set; }
        [JsonProperty(PropertyName = "error")]
        public string Error { get; set; }
    }
}
