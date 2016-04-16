using GetAroundAuckland.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetAroundAuckland.Services.SqlService
{
    public interface ISqlService
    {
        bool AddAgencies(List<Agency> agencies);
        bool AddCalendars(List<Calendar> calendars);
    }
}
