using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Web.Dtos;
using Web.Interfaces;

namespace Web.Clients
{
    /// <summary>
    /// Client to getting all episodes
    /// </summary>
    public class EpisodeClient : BaseClient, IEpisodeClient
    {
        public EpisodeClient(IHttpHandler client) : base(client)
        {
        }

        public async Task<List<EpisodeResultResponse>> GetEpisodesAsync(string url)
        {
            var result = new List<EpisodeResultResponse>();

            //Calling the first page of episodes
            var episodesResponse = await CallGetApiAsync<EpisodeResponse>(url);

            result.AddRange(episodesResponse.Results);

            if (string.IsNullOrWhiteSpace(episodesResponse.Info.Next)) return result;

            var pageUrl = episodesResponse.Info.Next;

            //Using a Parallel.For to call all episode pages
            var runner = Task.Run(() => Parallel.For(2, episodesResponse.Info.Pages+1, (index) =>
            {
                var newUrl = pageUrl.Replace("page=2", $"page={index}");

                var episodesPageResponse = CallGetApiAsync<EpisodeResponse>(newUrl).Result;

                lock (_lock)
                {
                    result.AddRange(episodesPageResponse.Results);
                }
            }));

            //Waiting until all opened threads, generated by Parallel.For, will closed
            runner.Wait();

            return result;
        }
    }
}
