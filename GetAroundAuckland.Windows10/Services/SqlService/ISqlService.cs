using GetAroundAuckland.Windows10.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetAroundAuckland.Windows10.Services.SqlService
{
    public interface ISqlService
    {
        Task<List<Route>> GetRoutes();
    }
}
