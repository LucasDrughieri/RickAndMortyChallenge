using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using Web.Dtos;
using Web.Interfaces;
using Web.Services;

namespace Tests.Services
{
    public class CharCounterServiceTests
    {
        private Mock<ICharacterResolver> characterResolver;
        private Mock<ILocationResolver> locationResolver;
        private Mock<IEpisodesResolver> episodesResolver;
        private Mock<IAvailableApisClient> availableApisClient;

        private CharCounterService charCounterService;

        [SetUp]
        public void Setup()
        {
            characterResolver = new Mock<ICharacterResolver>();
            locationResolver = new Mock<ILocationResolver>();
            episodesResolver = new Mock<IEpisodesResolver>();
            availableApisClient = new Mock<IAvailableApisClient>();

            availableApisClient.Setup(x => x.GetAvailableApisAsync()).ReturnsAsync(new AvailabeApisResponse { Characters = "", Episodes = "", Locations = "" });
            characterResolver.Setup(x => x.Execute(It.IsAny<string>())).ReturnsAsync("La letra c aparece 394 veces en los nombres de todos los personajes");
            locationResolver.Setup(x => x.Execute(It.IsAny<string>())).ReturnsAsync("La letra I aparece 115 veces en los nombres de todas las ubicaciones");
            episodesResolver.Setup(x => x.Execute(It.IsAny<string>())).ReturnsAsync("La letra e aparece 71 veces en los nombres de todos los episodios");

            charCounterService = new CharCounterService(characterResolver.Object, locationResolver.Object, episodesResolver.Object, availableApisClient.Object);
        }

        [Test]
        public async Task MustRunSuccessfully()
        {
            var response = await charCounterService.RunAsync();

            Assert.AreEqual(response.CharacterMessage, "La letra c aparece 394 veces en los nombres de todos los personajes");
            Assert.AreEqual(response.LocationMessage, "La letra I aparece 115 veces en los nombres de todas las ubicaciones");
            Assert.AreEqual(response.EpisodeMessage, "La letra e aparece 71 veces en los nombres de todos los episodios");
        }
    }
}
