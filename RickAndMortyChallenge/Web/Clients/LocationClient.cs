using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Web.Dtos;

namespace Web.Clients
{
    public class LocationClient : BaseClient
    {
        public LocationClient(HttpClient client) : base(client)
        {
        }

        public async Task<List<LocationResultResponse>> GetLocationsAsync(string url)
        {
            var result = new List<LocationResultResponse>();

            var locationResponse = await CallApiAsync<LocationResponse>(url);

            result.AddRange(locationResponse.Results);

            var pageUrl = locationResponse.Info.Next;

            var runner = Task.Run(() => Parallel.For(2, locationResponse.Info.Pages+1, (index) =>
            {
                var newUrl = pageUrl.Replace("page=2", $"page={index}");

                var locationPageResponse = CallApiAsync<LocationResponse>(newUrl).Result;

                lock (_lock)
                {
                    result.AddRange(locationPageResponse.Results);
                }
            }));

            runner.Wait();

            return result;
        }
    }
}
