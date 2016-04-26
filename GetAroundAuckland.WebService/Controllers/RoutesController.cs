using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using GetAroundAuckland.WebService.Services.SqlService;
using GetAroundAuckland.WebService.Models;

namespace GetAroundAuckland.WebService.Controllers
{
    [Route("api/[controller]")]
    public class RoutesController : Controller
    {
        private ISqlService _sqlService;

        public RoutesController(ISqlService sqlService)
        {
            _sqlService = sqlService;
        }

        // GET: api/routes
        [HttpGet]
        public IEnumerable<Route> Get()
        {
            var routes = _sqlService.GetRoutes();
            return routes;
        }

        // GET api/routes/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }        
    }
}
