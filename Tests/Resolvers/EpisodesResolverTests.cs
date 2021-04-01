using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Dtos;
using Web.Interfaces;
using Web.Resolvers;

namespace Tests.Resolvers
{
    public class EpisodesResolverTests
    {
        private EpisodesResolver episodesResolver;
        private Mock<IEpisodeClient> episodeClient;

        [SetUp]
        public void Setup()
        {
            episodeClient = new Mock<IEpisodeClient>();

            var episodeList = new List<EpisodeResultResponse> {
                new EpisodeResultResponse { Id = 1, Name = "Pilot", Episode = "S01E01", Characters = new []{ "https://rickandmortyapi.com/api/character/1" } },
                new EpisodeResultResponse { Id = 2, Name = "Lawnmower Dog", Episode = "S01E01", Characters = new []{ "https://rickandmortyapi.com/api/character/1" } },
                new EpisodeResultResponse { Id = 3, Name = "Anatomy Park", Episode = "S01E01", Characters = new []{ "https://rickandmortyapi.com/api/character/1" } },
                new EpisodeResultResponse { Id = 4, Name = "M. Night Shaym-Aliens!", Episode = "S01E01", Characters = new []{ "https://rickandmortyapi.com/api/character/1" } },
                new EpisodeResultResponse { Id = 5, Name = "Meeseeks and Destroy", Episode = "S01E01", Characters = new []{ "https://rickandmortyapi.com/api/character/1" } },
                new EpisodeResultResponse { Id = 6, Name = "", Episode = "S01E01", Characters = new []{ "https://rickandmortyapi.com/api/character/1" } },
                new EpisodeResultResponse { Id = 7, Name = null, Episode = "S01E01", Characters = new []{ "https://rickandmortyapi.com/api/character/1" } }
            };

            episodeClient.Setup(x => x.GetEpisodesAsync("url")).ReturnsAsync(episodeList);
            episodeClient.Setup(x => x.GetEpisodesAsync("")).ReturnsAsync(new List<EpisodeResultResponse>());

            episodesResolver = new EpisodesResolver(episodeClient.Object);
        }

        [Test]
        public async Task MustRunSuccessfully()
        {
            var episodeMessage = await episodesResolver.Execute("url");

            Assert.AreEqual(episodeMessage, "La letra e aparece 7 veces en los nombres de todos los episodios");
        }

        [Test]
        public async Task EpisodesAreEmpty()
        {
            var episodeMessage = await episodesResolver.Execute("");

            Assert.AreEqual(episodeMessage, "The episodes API returns an empty list");
        }
    }
}
