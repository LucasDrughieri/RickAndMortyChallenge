using Common.Clients;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CharCounter.Resolvers
{
    public class EpisodesResolver
    {
        const string letter = "e";

        private EpisodeClient episodeClient;

        public EpisodesResolver(EpisodeClient _episodeClient)
        {
            episodeClient = _episodeClient;
        }

        public async Task Execute(string charactersUrl)
        {
            var characters = await episodeClient.GetEpisodesAsync(charactersUrl);

            Console.WriteLine($"La letra {letter} aparece {characters.Sum(_ => _.GetLetterCount(letter))} veces en los nombres de todos los episodios");
        }
    }
}
