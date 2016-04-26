using GetAroundAuckland.WebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetAroundAuckland.WebService.Services.SqlService
{
    public interface ISqlService
    {
        List<Route> GetRoutes();
        List<Stop> GetStops();
        List<Agency> GetAgencies();
    }
}
