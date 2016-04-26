using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetAroundAuckland.Windows10.Models
{
    public class Route
    {
        public string Id { get; set; }
        public string AgencyId { get; set; }
        public string ShortName { get; set; }
        public string LongName { get; set; }
        public byte Type { get; set; }
        public string Color { get; set; }
        public string TextColor { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1} - {2}", AgencyId, ShortName, LongName);
        }
    }
}
