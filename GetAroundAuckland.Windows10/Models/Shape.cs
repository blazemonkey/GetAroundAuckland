using Newtonsoft.Json;
using SQLite.Net.Attributes;
using System.Collections.Generic;

namespace GetAroundAuckland.Windows10.Models
{
    public class Shape
    {
        [PrimaryKey]
        [JsonProperty(PropertyName = "shape_id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "shape_pt_lat")]
        public decimal Latitude { get; set; }
        [JsonProperty(PropertyName = "shape_pt_lon")]
        public decimal Longitude { get; set; }
        [PrimaryKey]
        [JsonProperty(PropertyName = "shape_pt_sequence")]
        public int Sequence { get; set; }
        [JsonProperty(PropertyName = "shape_dist_traveled")]
        public int? Distance { get; set; }
    }

    public class ShapeResponse
    {
        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }
        [JsonProperty(PropertyName = "response")]
        public List<Shape> Response { get; set; }
        [JsonProperty(PropertyName = "error")]
        public string Error { get; set; }
    }
}
