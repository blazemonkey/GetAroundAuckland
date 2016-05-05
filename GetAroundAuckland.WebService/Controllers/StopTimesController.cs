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
    public class StopTimesController : Controller
    {
        private ISqlService _sqlService;

        public StopTimesController(ISqlService sqlService)
        {
            _sqlService = sqlService;
        }

        [HttpGet("{id}")]
        public IEnumerable<StopTime> Get(string id)
        {
            var stopTimes = _sqlService.GetStopTimesByTripId(id);
            return stopTimes;
        }
    }
}
