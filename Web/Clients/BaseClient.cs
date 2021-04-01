using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Web.Interfaces;
using Web.Utils;

namespace Web.Clients
{
    public class BaseClient : IBaseClient
    {
        protected object _lock;
        protected readonly IHttpHandler _client;
        protected JsonSerializerOptions jsonSerializerOptions;

        public BaseClient(IHttpHandler client)
        {
            _client = client;
            _lock = new object();
            jsonSerializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true, };
        }

        /// <summary>
        /// Base method to do a HTTP GET
        /// </summary>
        /// <typeparam name="T">class to map the response</typeparam>
        /// <param name="url">url to call</param>
        public async Task<T> CallGetApiAsync<T>(string url) where T : class
        {
            using (HttpRequestMessage httpRequestMessage = new HttpRequestMessage())
            {
                httpRequestMessage.RequestUri = new Uri(url);
                httpRequestMessage.Method = HttpMethod.Get;

                using (HttpResponseMessage response = await _client.Get(httpRequestMessage))
                {
                    if (!response.IsSuccessStatusCode) throw new AppException($"Error calling GET {url}");

                    string responseBody = await response.Content.ReadAsStringAsync();

                    var data = JsonSerializer.Deserialize<T>(responseBody, jsonSerializerOptions);

                    return data;
                }
            }
        }
    }
}
