using Moq;
using NUnit.Framework;
using System.Net.Http;
using System.Threading.Tasks;
using Web.Clients;
using Web.Interfaces;

namespace Tests.Clients
{
    public class EpisodeClientTests
    {
        private EpisodeClient episodeClient;
        private Mock<IHttpHandler> httpClient;

        [SetUp]
        public void Setup()
        {
            httpClient = new Mock<IHttpHandler>();

            episodeClient = new EpisodeClient(httpClient.Object);
        }

        [Test]
        public async Task MustRunSuccessfullyWithTwoPages()
        {
            ConfigureHttpClientResponse(2);

            var episodes = await episodeClient.GetEpisodesAsync("http://google.com");

            Assert.AreEqual(10, episodes.Count);
        }

        [Test]
        public async Task MustRunSuccessfullyWithOnePage()
        {
            ConfigureHttpClientResponse(1);

            var episodes = await episodeClient.GetEpisodesAsync("http://google.com");

            Assert.AreEqual(5, episodes.Count);
        }

        private void ConfigureHttpClientResponse(int pages)
        {
            httpClient.Setup(x => x.Get(It.IsAny<HttpRequestMessage>())).ReturnsAsync((HttpRequestMessage request) =>
            {
                HttpResponseMessage response = new HttpResponseMessage();
                response.StatusCode = System.Net.HttpStatusCode.OK;
                response.Content = new StringContent(GetJsonEpisodeResponse(pages));
                return response;
            });
        }

        private string GetJsonEpisodeResponse(int pages)
        {
            return "{\n\"info\": {\n\"count\": 41,\n\"pages\": *,\n\"next\": \"https://rickandmortyapi.com/api/episode?page=2\",\n\"prev\": null\n},\n\"results\": [\n{\n\"id\": 1,\n\"name\": \"Pilot\",\n\"air_date\": \"December 2, 2013\",\n\"episode\": \"S01E01\",\n\"characters\": [\n\"https://rickandmortyapi.com/api/character/1\",\n\"https://rickandmortyapi.com/api/character/435\"\n],\n\"url\": \"https://rickandmortyapi.com/api/episode/1\",\n\"created\": \"2017-11-10T12:56:33.798Z\"\n},\n{\n\"id\": 2,\n\"name\": \"Lawnmower Dog\",\n\"air_date\": \"December 9, 2013\",\n\"episode\": \"S01E02\",\n\"characters\": [\n\"https://rickandmortyapi.com/api/character/1\",\n\"https://rickandmortyapi.com/api/character/405\"\n],\n\"url\": \"https://rickandmortyapi.com/api/episode/2\",\n\"created\": \"2017-11-10T12:56:33.916Z\"\n},\n{\n\"id\": 3,\n\"name\": \"Anatomy Park\",\n\"air_date\": \"December 16, 2013\",\n\"episode\": \"S01E03\",\n\"characters\": [\n\"https://rickandmortyapi.com/api/character/1\",\n\"https://rickandmortyapi.com/api/character/356\"\n],\n\"url\": \"https://rickandmortyapi.com/api/episode/3\",\n\"created\": \"2017-11-10T12:56:34.022Z\"\n},\n{\n\"id\": 4,\n\"name\": \"M. Night Shaym-Aliens!\",\n\"air_date\": \"January 13, 2014\",\n\"episode\": \"S01E04\",\n\"characters\": [\n\"https://rickandmortyapi.com/api/character/1\",\n\"https://rickandmortyapi.com/api/character/338\"\n],\n\"url\": \"https://rickandmortyapi.com/api/episode/4\",\n\"created\": \"2017-11-10T12:56:34.129Z\"\n},\n{\n\"id\": 5,\n\"name\": \"Meeseeks and Destroy\",\n\"air_date\": \"January 20, 2014\",\n\"episode\": \"S01E05\",\n\"characters\": [\n\"https://rickandmortyapi.com/api/character/1\",\n\"https://rickandmortyapi.com/api/character/400\"\n],\n\"url\": \"https://rickandmortyapi.com/api/episode/5\",\n\"created\": \"2017-11-10T12:56:34.236Z\"\n}\n]\n}".Replace("*", pages.ToString());
        }
    }
}
