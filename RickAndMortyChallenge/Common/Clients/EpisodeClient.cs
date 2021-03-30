using Common.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Clients
{
    public class EpisodeClient : BaseClient
    {
        public async Task<List<EpisodeResultResponse>> GetEpisodesAsync(string url)
        {
            var result = new List<EpisodeResultResponse>();

            var episodesResponse = await CallApiAsync<EpisodeResponse>(url);

            result.AddRange(episodesResponse.Results);

            var pageUrl = episodesResponse.Info.Next;

            var runner = Task.Run(() => Parallel.For(2, episodesResponse.Info.Pages, (index) =>
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
