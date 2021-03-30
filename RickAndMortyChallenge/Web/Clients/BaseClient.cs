using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

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

        protected async Task<T> CallApiAsync<T>(string url) where T : class
        {
            using (HttpRequestMessage httpRequestMessage = new HttpRequestMessage())
            {
                httpRequestMessage.RequestUri = new Uri(url);
                httpRequestMessage.Method = HttpMethod.Get;

                using (HttpResponseMessage response = await _client.SendAsync(httpRequestMessage))
                {
                    string responseBody = await response.Content.ReadAsStringAsync();

                    var data = JsonSerializer.Deserialize<T>(responseBody, jsonSerializerOptions);

                    return data;
                }
            }
        }
    }
}
