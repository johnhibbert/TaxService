using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaxService.Interfaces
{
    public interface IWebClient
    {
        Task<string> GetRequest(string url);

        Task<string> PostRequest(string url, Dictionary<string, string> properties);
    }
}
