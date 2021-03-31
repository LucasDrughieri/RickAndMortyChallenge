﻿using System.Linq;
using System.Threading.Tasks;
using Web.Clients;

namespace Web.Resolvers
{
    /// <summary>
    /// Class to resolve how many the letter e (case insensitive) appears in all episodes´s name
    /// </summary>
    public class EpisodesResolver
    {
        const string letter = "e";

        private EpisodeClient episodeClient;

        public EpisodesResolver(EpisodeClient _episodeClient)
        {
            episodeClient = _episodeClient;
        }

        public async Task<string> Execute(string episodesUrl)
        {
            //Calling episodeClient to get all episodes by http get
            var episodes = await episodeClient.GetEpisodesAsync(episodesUrl);

            if (!episodes.Any()) return "The episodes API returns an empty list";

            return $"La letra {letter} aparece {episodes.Sum(_ => _.GetLetterCount(letter))} veces en los nombres de todos los episodios";
        }
    }
}