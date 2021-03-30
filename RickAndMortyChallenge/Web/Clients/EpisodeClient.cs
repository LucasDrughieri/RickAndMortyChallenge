using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Web.Dtos;

namespace Web.Clients
{
    public class EpisodeClient : BaseClient
    {
        public EpisodeClient(HttpClient client) : base(client)
        {
        }

        public async Task<List<EpisodeResultResponse>> GetEpisodesAsync(string url)
        {
            var result = new List<EpisodeResultResponse>();

            var episodesResponse = await CallApiAsync<EpisodeResponse>(url);

            result.AddRange(episodesResponse.Results);

            var pageUrl = episodesResponse.Info.Next;

            var runner = Task.Run(() => Parallel.For(2, episodesResponse.Info.Pages+1, (index) =>
            {
                var newUrl = pageUrl.Replace("page=2", $"page={index}");

                var episodesPageResponse = CallApiAsync<EpisodeResponse>(newUrl).Result;

                lock (_lock)
                {
                    result.AddRange(episodesPageResponse.Results);
                }
            }));

            runner.Wait();

            return result;
        }
    }
}
