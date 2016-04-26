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
    public class AgenciesController : Controller
    {
        private ISqlService _sqlService;

        public AgenciesController(ISqlService sqlService)
        {
            _sqlService = sqlService;
        }

        // GET: api/routes
        [HttpGet]
        public IEnumerable<Agency> Get()
        {
            var agencies = _sqlService.GetAgencies();
            return agencies;
        }

        // GET api/routes/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
    }
}
