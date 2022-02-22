using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TaxService.Interfaces;

namespace TaxService.WebClients
{
    public class WebClient : IWebClient
    {
        string _apiKey;
        private readonly HttpClient _httpClient;

        public WebClient(string apiKey)
        {
            _apiKey = apiKey ?? throw new ArgumentNullException(nameof(apiKey));
            if (string.IsNullOrWhiteSpace(_apiKey))
            {
                throw new ArgumentException($"Argument {nameof(apiKey)} cannot be empty or whitespace;");
            }

            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
        }

        public async Task<string> GetRequest(string url)
        {
            var responseString = await _httpClient.GetStringAsync(url);
            return responseString;
        }

        public async Task<string> PostRequest(string url, Dictionary<string, string> props)
        {
            //https://stackoverflow.com/questions/4015324/how-to-make-an-http-post-web-request

            var content = new FormUrlEncodedContent(props);
            var response = await _httpClient.PostAsync(url, content);
            var responseString = await response.Content.ReadAsStringAsync();

            return responseString;
        }
    }
}
