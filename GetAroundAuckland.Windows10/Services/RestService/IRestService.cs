using System.Collections.Generic;
using System.Threading.Tasks;

namespace GetAroundAuckland.Windows10.Services.RestService
{
    public interface IRestService
    {
        Task<T> GetApi<T>(string apiUrl, string resourceUrl, List<KeyValuePair<string, string>> parameters = null);
    }
}
