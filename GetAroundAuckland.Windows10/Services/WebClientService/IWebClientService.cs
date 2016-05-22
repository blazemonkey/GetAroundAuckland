using GetAroundAuckland.Windows10.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetAroundAuckland.Windows10.Services.WebClientService
{
    public interface IWebClientService
    {
        Task<IEnumerable<CalendarDate>> GetCalendarDates();
        Task<IEnumerable<Movement>> GetStopLiveData(int stopCode);
    }
}
