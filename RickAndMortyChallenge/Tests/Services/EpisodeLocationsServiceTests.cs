using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Dtos;
using Web.Interfaces;
using Web.Services;

namespace Tests.Services
{
    public class EpisodeLocationsServiceTests
    {
        private Mock<ICharacterClient> characterClient;
        private Mock<IEpisodeClient> episodesClient;
        private Mock<IAvailableApisClient> availableApisClient;

        private EpisodeLocationsService episodeLocationsService;

        [SetUp]
        public void Setup()
        {
            characterClient = new Mock<ICharacterClient>();
            episodesClient = new Mock<IEpisodeClient>();
            availableApisClient = new Mock<IAvailableApisClient>();

            ConfigureCharacterClient();
            ConfigureEpisodeClient();

            availableApisClient.Setup(x => x.GetAvailableApisAsync()).ReturnsAsync(new AvailabeApisResponse { Characters = "https://rickandmortyapi.com/api/character", Episodes = "https://rickandmortyapi.com/api/episode", Locations = "https://rickandmortyapi.com/api/location" });

            episodeLocationsService = new EpisodeLocationsService(characterClient.Object, episodesClient.Object, availableApisClient.Object);
        }

        private void ConfigureEpisodeClient()
        {
            var episodeList = new List<EpisodeResultResponse> {
                new EpisodeResultResponse { Id = 1, Name = "Pilot", Episode = "S01E01", Characters = new []{ "https://rickandmortyapi.com/api/character/1", "https://rickandmortyapi.com/api/character/2", "https://rickandmortyapi.com/api/character/3" } },
                new EpisodeResultResponse { Id = 2, Name = "Lawnmower Dog", Episode = "S01E02", Characters = new []{ "https://rickandmortyapi.com/api/character/2" } },
                new EpisodeResultResponse { Id = 3, Name = "Anatomy Park", Episode = "S01E03", Characters = new []{ "https://rickandmortyapi.com/api/character/3", "https://rickandmortyapi.com/api/character/4" } },
                new EpisodeResultResponse { Id = 4, Name = "M. Night Shaym-Aliens!", Episode = "S01E04", Characters = new []{ "https://rickandmortyapi.com/api/character/4", "https://rickandmortyapi.com/api/character/5" } },
            };

            episodesClient.Setup(x => x.GetEpisodesAsync(It.IsAny<string>())).ReturnsAsync(episodeList);
        }

        private void ConfigureCharacterClient()
        {
            var characterList = new List<CharacterResultResponse> {
                new CharacterResultResponse { Id = 1, Name = "Rick Sanchez", Location = new LocationItemResponse { Name = "Earth (Replacement Dimension)", Url = "https://rickandmortyapi.com/api/location/20" }, Origin = new LocationItemResponse { Name = "Earth (C-137)", Url = "https://rickandmortyapi.com/api/location/3" } },
                new CharacterResultResponse { Id = 2, Name = "Morty Smith", Location = new LocationItemResponse { Name = "Earth (Replacement Dimension)", Url = "https://rickandmortyapi.com/api/location/1" }, Origin = new LocationItemResponse { Name = "Earth (C-137)", Url = "https://rickandmortyapi.com/api/location/6" } },
                new CharacterResultResponse { Id = 3, Name = "Summer Smith", Location = new LocationItemResponse { Name = "Earth (Replacement Dimension)", Url = "https://rickandmortyapi.com/api/location/4" }, Origin = new LocationItemResponse { Name = "Earth (C-137)", Url = "https://rickandmortyapi.com/api/location/7" } },
                new CharacterResultResponse { Id = 4, Name = "Beth Smith", Location = new LocationItemResponse { Name = "Earth (Replacement Dimension)", Url = "https://rickandmortyapi.com/api/location/24" }, Origin = new LocationItemResponse { Name = "Earth (C-137)", Url = "https://rickandmortyapi.com/api/location/11" } },
                new CharacterResultResponse { Id = 5, Name = "Abadango Cluster Princess", Location = new LocationItemResponse { Name = "Earth (Replacement Dimension)", Url = "https://rickandmorapi/location/24" }, Origin = new LocationItemResponse { Name = "Earth (C-137)", Url = "https://rickandmortyapi.com/api/location/11" } }
            };

            characterClient.Setup(x => x.GetCharactersAsync(It.IsAny<string>())).ReturnsAsync(characterList);
        }

        [Test]
        public async Task MustRunSuccessfully()
        {
            var response = await episodeLocationsService.RunAsync();

            var responseToCompare = BuildSuccessResponseToCompare();

            Assert.AreEqual(responseToCompare.Episodes[0].Id, response.Episodes[0].Id);
            Assert.AreEqual(responseToCompare.Episodes[0].Episode, response.Episodes[0].Episode);
            Assert.AreEqual(responseToCompare.Episodes[0].TotalCharacters, response.Episodes[0].TotalCharacters);
            Assert.AreEqual(responseToCompare.Episodes[0].Locations.Count, response.Episodes[0].Locations.Count);

            Assert.AreEqual(responseToCompare.Episodes[1].Id, response.Episodes[1].Id);
            Assert.AreEqual(responseToCompare.Episodes[1].Episode, response.Episodes[1].Episode);
            Assert.AreEqual(responseToCompare.Episodes[1].TotalCharacters, response.Episodes[1].TotalCharacters);
            Assert.AreEqual(responseToCompare.Episodes[1].Locations.Count, response.Episodes[1].Locations.Count);

            Assert.AreEqual(responseToCompare.Episodes[2].Id, response.Episodes[2].Id);
            Assert.AreEqual(responseToCompare.Episodes[2].Episode, response.Episodes[2].Episode);
            Assert.AreEqual(responseToCompare.Episodes[2].TotalCharacters, response.Episodes[2].TotalCharacters);
            Assert.AreEqual(responseToCompare.Episodes[2].Locations.Count, response.Episodes[2].Locations.Count);

            Assert.AreEqual(responseToCompare.Episodes[3].Id, response.Episodes[3].Id);
            Assert.AreEqual(responseToCompare.Episodes[3].Episode, response.Episodes[3].Episode);
            Assert.AreEqual(responseToCompare.Episodes[3].TotalCharacters, response.Episodes[3].TotalCharacters);
            Assert.AreEqual(responseToCompare.Episodes[3].Locations.Count, response.Episodes[3].Locations.Count);
        }

        private EpisodeLocationsResponse BuildSuccessResponseToCompare()
        {
            var response = new EpisodeLocationsResponse
            {
                Episodes = new List<EpisodeLocationItem>
                {
                    new EpisodeLocationItem {
                        Id = 1,
                        Episode = "S01E01",
                        TotalCharacters = 3,
                        Locations = new List<LocationItem>
                            { new LocationItem { Id = 20, Name = "Earth (Replacement Dimension)" },
                            { new LocationItem { Id = 1, Name = "Earth (Replacement Dimension)" } },
                            { new LocationItem { Id = 4, Name = "Earth (Replacement Dimension)" } }
                        },
                        Origins = new List<LocationItem>
                            { new LocationItem { Id = 3, Name = "Earth (C-137)" },
                            { new LocationItem { Id = 6, Name = "Earth (C-137)" } },
                            { new LocationItem { Id = 7, Name = "Earth (C-137)" } }
                        },
                    },

                    new EpisodeLocationItem {
                        Id = 2,
                        Episode = "S01E02",
                        TotalCharacters = 1,
                        Locations = new List<LocationItem>
                        { 
                            new LocationItem { Id = 1, Name = "Earth (Replacement Dimension)" }
                        },
                         Origins = new List<LocationItem>
                        { 
                             new LocationItem { Id = 6, Name = "Earth (C-137)" }
                        },
                    },

                    new EpisodeLocationItem {
                        Id = 3,
                        Episode = "S01E03",
                        TotalCharacters = 2,
                        Locations = new List<LocationItem>
                            { new LocationItem { Id = 24, Name = "Earth (Replacement Dimension)" },
                            { new LocationItem { Id = 4, Name = "Earth (Replacement Dimension)" } }
                        },
                        Origins = new List<LocationItem>
                            { new LocationItem { Id = 11, Name = "Earth (C-137)" },
                            { new LocationItem { Id = 7, Name = "Earth (C-137)" } }
                        },
                    },

                    new EpisodeLocationItem {
                        Id = 4,
                        Episode = "S01E04",
                        TotalCharacters = 2,
                        Locations = new List<LocationItem>
                        {
                            new LocationItem { Id = 11, Name = "Earth (Replacement Dimension)" }
                        },
                         Origins = new List<LocationItem>
                        {
                             new LocationItem { Id = 24, Name = "Earth (C-137)" }
                        },
                    },
                }
            };

            return response;
        }
    }
}
