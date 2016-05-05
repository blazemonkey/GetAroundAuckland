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
    public class CalendarsController : Controller
    {
        private ISqlService _sqlService;

        public CalendarsController(ISqlService sqlService)
        {
            _sqlService = sqlService;
        }

        [HttpGet("{id}")]
        public IEnumerable<Calendar> Get(string id)
        {
            var calendars = _sqlService.GetCalendarsByServiceId(id);
            return calendars;
        }
    }
}
