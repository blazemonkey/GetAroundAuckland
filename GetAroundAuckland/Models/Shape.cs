using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetAroundAuckland.Models
{
    public class Shape
    {
        public string Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Sequence { get; set; }
        public int? Distance { get; set; }
    }

    public sealed class ShapeMap : CsvClassMap<Shape>
    {
        public ShapeMap()
        {
            Map(m => m.Id).Index(0);
            Map(m => m.Latitude).Index(1);
            Map(m => m.Longitude).Index(2);
            Map(m => m.Sequence).Index(3);
            Map(m => m.Distance).Index(4);
        }
    }
}
