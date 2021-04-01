using Moq;
using NUnit.Framework;
using System.Net.Http;
using System.Threading.Tasks;
using Web.Clients;
using Web.Interfaces;

namespace Tests.Clients
{
    public class LocationClientTests
    {
        private LocationClient locationClient;
        private Mock<IHttpHandler> httpClient;

        [SetUp]
        public void Setup()
        {
            httpClient = new Mock<IHttpHandler>();

            locationClient = new LocationClient(httpClient.Object);
        }

        [Test]
        public async Task MustRunSuccessfullyWithTwoPages()
        {
            ConfigureHttpClientResponse(2);

            var locations = await locationClient.GetLocationsAsync("http://google.com");

            Assert.AreEqual(10, locations.Count);
        }

        [Test]
        public async Task MustRunSuccessfullyWithOnePage()
        {
            ConfigureHttpClientResponse(1);

            var locations = await locationClient.GetLocationsAsync("http://google.com");

            Assert.AreEqual(5, locations.Count);
        }

        private void ConfigureHttpClientResponse(int pages)
        {
            httpClient.Setup(x => x.Get(It.IsAny<HttpRequestMessage>())).ReturnsAsync((HttpRequestMessage request) =>
            {
                HttpResponseMessage response = new HttpResponseMessage();
                response.StatusCode = System.Net.HttpStatusCode.OK;
                response.Content = new StringContent(GetJsonLocationResponse(pages));
                return response;
            });
        }

        [Test]
        public async Task MustRunSuccessfullyWithoutNextPage()
        {
            httpClient.Setup(x => x.Get(It.IsAny<HttpRequestMessage>())).ReturnsAsync((HttpRequestMessage request) =>
            {
                HttpResponseMessage response = new HttpResponseMessage();
                response.StatusCode = System.Net.HttpStatusCode.OK;
                response.Content = new StringContent(GetJsonLocationResponseWithNextPageNull());
                return response;
            });

            var locations = await locationClient.GetLocationsAsync("http://google.com");

            Assert.AreEqual(5, locations.Count);
        }

        private string GetJsonLocationResponse(int pages)
        {
            return "{\n\"info\": {\n\"count\": 108,\n\"pages\": *,\n\"next\": \"https://rickandmortyapi.com/api/location?page=2\",\n\"prev\": null\n},\n\"results\": [\n{\n\"id\": 1,\n\"name\": \"Earth (C-137)\",\n\"type\": \"Planet\",\n\"dimension\": \"Dimension C-137\",\n\"residents\": [\n\"https://rickandmortyapi.com/api/character/38\"\n],\n\"url\": \"https://rickandmortyapi.com/api/location/1\",\n\"created\": \"2017-11-10T12:42:04.162Z\"\n},\n{\n\"id\": 2,\n\"name\": \"Abadango\",\n\"type\": \"Cluster\",\n\"dimension\": \"unknown\",\n\"residents\": [\n\"https://rickandmortyapi.com/api/character/6\"\n],\n\"url\": \"https://rickandmortyapi.com/api/location/2\",\n\"created\": \"2017-11-10T13:06:38.182Z\"\n},\n{\n\"id\": 3,\n\"name\": \"Citadel of Ricks\",\n\"type\": \"Space station\",\n\"dimension\": \"unknown\",\n\"residents\": [\n\"https://rickandmortyapi.com/api/character/8\",\n\"https://rickandmortyapi.com/api/character/489\"\n],\n\"url\": \"https://rickandmortyapi.com/api/location/3\",\n\"created\": \"2017-11-10T13:08:13.191Z\"\n},\n{\n\"id\": 4,\n\"name\": \"Worldender's lair\",\n\"type\": \"Planet\",\n\"dimension\": \"unknown\",\n\"residents\": [\n\"https://rickandmortyapi.com/api/character/10\",\n\"https://rickandmortyapi.com/api/character/395\"\n],\n\"url\": \"https://rickandmortyapi.com/api/location/4\",\n\"created\": \"2017-11-10T13:08:20.569Z\"\n},\n{\n\"id\": 5,\n\"name\": \"Anatomy Park\",\n\"type\": \"Microverse\",\n\"dimension\": \"Dimension C-137\",\n\"residents\": [\n\"https://rickandmortyapi.com/api/character/12\",\n\"https://rickandmortyapi.com/api/character/300\"\n],\n\"url\": \"https://rickandmortyapi.com/api/location/5\",\n\"created\": \"2017-11-10T13:08:46.060Z\"\n}\n]\n}".Replace("*", pages.ToString());
        }

        private string GetJsonLocationResponseWithNextPageNull()
        {
            return "{\n\"info\": {\n\"count\": 108,\n\"pages\": 1,\n\"next\": null,\n\"prev\": null\n},\n\"results\": [\n{\n\"id\": 1,\n\"name\": \"Earth (C-137)\",\n\"type\": \"Planet\",\n\"dimension\": \"Dimension C-137\",\n\"residents\": [\n\"https://rickandmortyapi.com/api/character/38\"\n],\n\"url\": \"https://rickandmortyapi.com/api/location/1\",\n\"created\": \"2017-11-10T12:42:04.162Z\"\n},\n{\n\"id\": 2,\n\"name\": \"Abadango\",\n\"type\": \"Cluster\",\n\"dimension\": \"unknown\",\n\"residents\": [\n\"https://rickandmortyapi.com/api/character/6\"\n],\n\"url\": \"https://rickandmortyapi.com/api/location/2\",\n\"created\": \"2017-11-10T13:06:38.182Z\"\n},\n{\n\"id\": 3,\n\"name\": \"Citadel of Ricks\",\n\"type\": \"Space station\",\n\"dimension\": \"unknown\",\n\"residents\": [\n\"https://rickandmortyapi.com/api/character/8\",\n\"https://rickandmortyapi.com/api/character/489\"\n],\n\"url\": \"https://rickandmortyapi.com/api/location/3\",\n\"created\": \"2017-11-10T13:08:13.191Z\"\n},\n{\n\"id\": 4,\n\"name\": \"Worldender's lair\",\n\"type\": \"Planet\",\n\"dimension\": \"unknown\",\n\"residents\": [\n\"https://rickandmortyapi.com/api/character/10\",\n\"https://rickandmortyapi.com/api/character/395\"\n],\n\"url\": \"https://rickandmortyapi.com/api/location/4\",\n\"created\": \"2017-11-10T13:08:20.569Z\"\n},\n{\n\"id\": 5,\n\"name\": \"Anatomy Park\",\n\"type\": \"Microverse\",\n\"dimension\": \"Dimension C-137\",\n\"residents\": [\n\"https://rickandmortyapi.com/api/character/12\",\n\"https://rickandmortyapi.com/api/character/300\"\n],\n\"url\": \"https://rickandmortyapi.com/api/location/5\",\n\"created\": \"2017-11-10T13:08:46.060Z\"\n}\n]\n}";
        }
    }
}
