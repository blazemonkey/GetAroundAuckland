using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetAroundAuckland.Windows10.Models
{
    public class Agency
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string TimeZone { get; set; }
        public string Lang { get; set; }
        public string Phone { get; set; }

        public static Agency CreateDefault()
        {
            return new Agency { Id = "All", Name = "all agencies" };
        }
    }
}
