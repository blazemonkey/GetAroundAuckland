using GetAroundAuckland.Windows10.Models;
using Microsoft.Practices.Unity;
using Services.JsonService;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Web.Http;

namespace GetAroundAuckland.Windows10.Services.RestService
{
    public class RestService : IRestService
    {
        [Dependency]
        public IJsonService JsonService { get; set; }

        private string _url = "https://api.at.govt.nz/v2/gtfs/";

        private async Task<T> GetApi<T>(string apiUrl, string resourceUrl, List<KeyValuePair<string, string>> parameters = null)
        {
            var client = new HttpClient();
            string paramString = string.Empty;

            if (parameters != null)
            {
                foreach (var param in parameters)
                {
                    paramString = paramString + param.Key + "/" + param.Value;
                }
            }

            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(apiUrl + resourceUrl + "/" + paramString),
                Method = HttpMethod.Get,
            };
            request.Headers.Add("Ocp-Apim-Subscription-Key", "633dea42ee4c4a46a7ff49d70921664d");
            var response = await client.SendRequestAsync(request);

            if (!response.IsSuccessStatusCode)
                return default(T);

            var contentString = await response.Content.ReadAsStringAsync();
            var content = JsonService.Deserialize<T>(contentString);

            return content;
        }

        public async Task<IEnumerable<Agency>> GetAgencies()
        {
            var agencies = await GetApi<AgencyResponse>(_url, "agency");
            if (CheckOk(agencies.Status))
                return agencies.Response;

            return null;
        }

        public async Task<IEnumerable<Route>> GetRoutes()
        {
            var agencies = await GetApi<RouteResponse>(_url, "routes");
            if (CheckOk(agencies.Status))
                return agencies.Response;

            return null;
        }

        public async Task<IEnumerable<Stop>> GetStops()
        {
            var agencies = await GetApi<StopResponse>(_url, "stops");
            if (CheckOk(agencies.Status))
                return agencies.Response;

            return null;
        }

        public async Task<IEnumerable<Trip>> GetTripsByRouteId(string routeId)
        {
            var parameters = new List<KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("routeid", routeId));
            var trips = await GetApi<TripResponse>(_url, "trips", parameters);
            if (CheckOk(trips.Status))
                return trips.Response;

            return null;
        }

        public async Task<IEnumerable<StopTime>> GetStopTimesByTripId(string tripId)
        {
            var parameters = new List<KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("tripId", tripId));
            var stopTimes = await GetApi<StopTimeResponse>(_url, "stopTimes", parameters);
            if (CheckOk(stopTimes.Status))
                return stopTimes.Response;

            return null;
        }

        public async Task<IEnumerable<Calendar>> GetCalendarsByServiceId(string serviceId)
        {
            var parameters = new List<KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("serviceId", serviceId));
            var calendars = await GetApi<CalendarResponse>(_url, "calendar", parameters);
            if (CheckOk(calendars.Status))
                return calendars.Response;

            return null;
        }

        public async Task<IEnumerable<CalendarDate>> GetCalendarDatesByServiceId(string serviceId)
        {
            var parameters = new List<KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("serviceId", serviceId));
            var calendarDates = await GetApi<CalendarDateResponse>(_url, "calendarDate", parameters);
            if (CheckOk(calendarDates.Status))
                return calendarDates.Response;

            return null;
        }

        public async Task<IEnumerable<Shape>> GetShapesById(string shapeId)
        {
            var parameters = new List<KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("shapeId", shapeId));
            var shapes = await GetApi<ShapeResponse>(_url, "shapes", parameters);
            if (CheckOk(shapes.Status))
                return shapes.Response;

            return null;
        }

        public async Task<IEnumerable<Route>> GetRoutesByStopId(string stopId)
        {
            var parameters = new List<KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("stopId", stopId));
            var routes = await GetApi<RouteResponse>(_url, "routes", parameters);
            if (CheckOk(routes.Status))
                return routes.Response;

            return null;
        }

        private bool CheckOk(string status)
        {
            return status == "OK";
        }
    }
}
