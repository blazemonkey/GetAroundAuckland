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
    public class CalendarDatesController : Controller
    {
        private ISqlService _sqlService;

        public CalendarDatesController(ISqlService sqlService)
        {
            _sqlService = sqlService;
        }

        [HttpGet("{id}")]
        public IEnumerable<CalendarDate> Get(string id)
        {
            var dates = _sqlService.GetCalendarDatesByServiceId(id);
            return dates;
        }
    }
}
