using System;
using System.Linq;
using System.Threading.Tasks;
using Web.Clients;

namespace Web.Resolvers
{
    public class EpisodesResolver
    {
        const string letter = "e";

        private EpisodeClient episodeClient;

        public EpisodesResolver(EpisodeClient _episodeClient)
        {
            episodeClient = _episodeClient;
        }

        public async Task<string> Execute(string charactersUrl)
        {
            var characters = await episodeClient.GetEpisodesAsync(charactersUrl);

            return $"La letra {letter} aparece {characters.Sum(_ => _.GetLetterCount(letter))} veces en los nombres de todos los episodios";
        }
    }
}
