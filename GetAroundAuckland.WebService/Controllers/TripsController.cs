using GetAroundAuckland.WebService.Models;
using GetAroundAuckland.WebService.Services.SqlService;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetAroundAuckland.WebService.Controllers
{
    [Route("api/[controller]")]
    public class TripsController : Controller
    {
        private ISqlService _sqlService;

        public TripsController(ISqlService sqlService)
        {
            _sqlService = sqlService;
        }

        // GET api/trips/5
        [HttpGet("{id}")]
        public IEnumerable<Trip> Get(string id)
        {
            var trips = _sqlService.GetTripsByRouteId(id);
            return trips;
        }
    }
}
