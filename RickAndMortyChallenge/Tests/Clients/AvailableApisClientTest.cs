using Moq;
using NUnit.Framework;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Web.Clients;
using Web.Interfaces;
using Web.Utils;

namespace Tests.Clients
{
    public class AvailableApisClientTests
    {
        private AvailableApisClient availableApisClient;
        private Mock<IHttpHandler> httpClient;

        [SetUp]
        public void Setup()
        {
            httpClient = new Mock<IHttpHandler>();

            availableApisClient = new AvailableApisClient(httpClient.Object);
        }

        [Test]
        public async Task MustRunSuccessfully()
        {
            httpClient.Setup(x => x.Get(It.IsAny<HttpRequestMessage>())).ReturnsAsync((HttpRequestMessage request) =>
            {
                HttpResponseMessage response = new HttpResponseMessage();
                response.StatusCode = System.Net.HttpStatusCode.OK;
                response.Content = new StringContent(GetSuccessResponse());
                return response;
            });

            var availabeApisResponse = await availableApisClient.GetAvailableApisAsync();

            Assert.AreEqual("https://rickandmortyapi.com/api/character", availabeApisResponse.Characters);
            Assert.AreEqual("https://rickandmortyapi.com/api/location", availabeApisResponse.Locations);
            Assert.AreEqual("https://rickandmortyapi.com/api/episode", availabeApisResponse.Episodes);
        }


        [Test]
        public void MustThrowAnAppException()
        {
            httpClient.Setup(x => x.Get(It.IsAny<HttpRequestMessage>())).ReturnsAsync((HttpRequestMessage request) =>
            {
                HttpResponseMessage response = new HttpResponseMessage();
                response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                response.Content = new StringContent("");
                return response;
            });

            var ex = Assert.Throws<AggregateException>(() =>
            {
                var response = availableApisClient.GetAvailableApisAsync().Result;
            });

            Assert.That(ex.Message, Is.EqualTo("One or more errors occurred. (Error calling GET https://rickandmortyapi.com/api)"));
        }

        private string GetSuccessResponse()
        {
            return "{\n    \"characters\": \"https://rickandmortyapi.com/api/character\",\n    \"locations\": \"https://rickandmortyapi.com/api/location\",\n    \"episodes\": \"https://rickandmortyapi.com/api/episode\"\n}";
        }
    }
}
