using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Dtos;
using Web.Interfaces;
using Web.Resolvers;

namespace Tests.Resolvers
{
    public class LocationResolverTests
    {
        private LocationResolver locationResolver;
        private Mock<ILocationClient> locationClient;

        [SetUp]
        public void Setup()
        {
            locationClient = new Mock<ILocationClient>();

            var locationList = new List<LocationResultResponse> {
                new LocationResultResponse { Id = 1, Name = "Earth (C-137)" },
                new LocationResultResponse { Id = 2, Name = "Abadango" },
                new LocationResultResponse { Id = 3, Name = "Citadel of Ricks" },
                new LocationResultResponse { Id = 4, Name = "Worldender's lair" },
                new LocationResultResponse { Id = 5, Name = "Anatomy Park" },
                new LocationResultResponse { Id = 6, Name = "" },
                new LocationResultResponse { Id = 7, Name = null }
            };

            locationClient.Setup(x => x.GetLocationsAsync("url")).ReturnsAsync(locationList);
            locationClient.Setup(x => x.GetLocationsAsync("")).ReturnsAsync(new List<LocationResultResponse>());

            locationResolver = new LocationResolver(locationClient.Object);
        }

        [Test]
        public async Task MustRunSuccessfully()
        {
            var locationMessage = await locationResolver.Execute("url");

            Assert.AreEqual(locationMessage, "La letra I aparece 3 veces en los nombres de todas las ubicaciones");
        }

        [Test]
        public async Task LocationsAreEmpty()
        {
            var locationMessage = await locationResolver.Execute("");

            Assert.AreEqual(locationMessage, "The locations API returns an empty list");
        }
    }
}
