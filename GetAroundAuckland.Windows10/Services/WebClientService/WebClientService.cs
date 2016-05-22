using GetAroundAuckland.Windows10.Models;
using Microsoft.Practices.Unity;
using Services.JsonService;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GetAroundAuckland.Windows10.Services.WebClientService
{
    public class WebClientService : IWebClientService
    {
        [Dependency]
        public IJsonService JsonService { get; set; }

        private async Task<string> DownloadFile(string url)
        {
            try
            {
                var result = string.Empty;
                using (var client = new HttpClient())
                {
                    result = await client.GetStringAsync(url);
                }

                return result;
            }
            catch (Exception e)
            {
                return string.Empty;
            }
        }

        public async Task<IEnumerable<CalendarDate>> GetCalendarDates()
        {
            var result = await DownloadFile("https://cdn01.at.govt.nz/data/calendar_dates.txt");
            if (result == string.Empty)
                return null;

            // parse, should probably make a proper csv parser
            var calendarDates = new List<CalendarDate>();
            var records = result.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

            foreach (var record in records.Skip(1))
            {
                var fields = record.Split(',');
                var calendarDate = new CalendarDate();
                for (var i = 0; i < fields.Count(); i++)
                { 
                    switch (i)
                    {
                        case 0:
                            calendarDate.ServiceId = fields[0];
                            break;
                        case 1:
                            calendarDate.Date = fields[1];
                            break;
                        case 2:
                            calendarDate.ExceptionType = Byte.Parse(fields[2]);
                            break;
                    }
                }
                calendarDates.Add(calendarDate);
            }

            return calendarDates;
        }

        public async Task<IEnumerable<Movement>> GetStopLiveData(int stopCode)
        {
            var result = await DownloadFile("http://api.maxx.co.nz/RealTime/v2/Departures/Stop/" + stopCode.ToString());
            if (result == string.Empty)
                return null;

            var movements = new List<Movement>();
            var response = JsonService.Deserialize<MovementResponse>(result);
            if (response.Error != null)
                return movements;

            movements = response.Movements;
            return movements;
        }
    }
}
