using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Web.Utils;

namespace Web.Clients
{
    public class BaseClient
    {
        protected object _lock;
        protected readonly HttpClient _client;
        protected JsonSerializerOptions jsonSerializerOptions;

        public BaseClient(HttpClient client)
        {
            _client = client;
            _lock = new object();
            jsonSerializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true, };

            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        /// <summary>
        /// Base method to do a HTTP GET
        /// </summary>
        /// <typeparam name="T">class to map the response</typeparam>
        /// <param name="url">url to call</param>
        protected async Task<T> CallGetApiAsync<T>(string url) where T : class
        {
            using (HttpRequestMessage httpRequestMessage = new HttpRequestMessage())
            {
                httpRequestMessage.RequestUri = new Uri(url);
                httpRequestMessage.Method = HttpMethod.Get;

                using (HttpResponseMessage response = await _client.SendAsync(httpRequestMessage))
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
