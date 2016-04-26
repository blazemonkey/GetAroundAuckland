using GetAroundAuckland.Windows10.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetAroundAuckland.Windows10.Interfaces
{
    public interface IMainPageViewModel
    {
        List<Route> FilterRoutes(string routeNo);
        List<Stop> FilterStops(string stopNo);
    }
}
