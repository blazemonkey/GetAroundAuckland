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
    public class ShapesController : Controller
    {
        private ISqlService _sqlService;

        public ShapesController(ISqlService sqlService)
        {
            _sqlService = sqlService;
        }

        // GET api/routes/5
        [HttpGet("{id}")]
        public IEnumerable<Shape> Get(string id)
        {
            var shapes = _sqlService.GetShapesById(id);
            return shapes;
        }
    }
}
