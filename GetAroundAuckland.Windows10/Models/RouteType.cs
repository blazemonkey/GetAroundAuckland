using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetAroundAuckland.Windows10.Models
{
    public class RouteType
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static List<RouteType> GetRouteTypes()
        {
            var routeTypes = new List<RouteType>();
            routeTypes.Add(new RouteType { Id = 2, Name = "train" });
            routeTypes.Add(new RouteType { Id = 3, Name = "bus" });
            routeTypes.Add(new RouteType { Id = 4, Name = "ferry" });
            return routeTypes;
        }
    }
}
