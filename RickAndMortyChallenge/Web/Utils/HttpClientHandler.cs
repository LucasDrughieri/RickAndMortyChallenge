using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Web.Interfaces;

namespace Web.Utils
{
    public class HttpClientHandler : IHttpHandler
    {
        private readonly HttpClient _client;

        public HttpClientHandler(HttpClient client)
        {
            _client = client;

            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<HttpResponseMessage> Get(HttpRequestMessage httpRequestMessage)
        {
            return await _client.SendAsync(httpRequestMessage);
        }
    }
}
