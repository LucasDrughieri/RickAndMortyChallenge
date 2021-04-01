using Moq;
using NUnit.Framework;
using System.Net.Http;
using System.Threading.Tasks;
using Web.Clients;
using Web.Interfaces;

namespace Tests.Clients
{
    public class CharacterClientTests
    {
        private CharacterClient characterClient;
        private Mock<IHttpHandler> httpClient;

        [SetUp]
        public void Setup()
        {
            httpClient = new Mock<IHttpHandler>();

            characterClient = new CharacterClient(httpClient.Object);
        }

        [Test]
        public async Task MustRunSuccessfullyWithTwoPages()
        {
            ConfigureHttpClientResponse(2);

            var characters = await characterClient.GetCharactersAsync("http://google.com");

            Assert.AreEqual(20, characters.Count);
        }

        [Test]
        public async Task MustRunSuccessfullyWithOnePage()
        {
            ConfigureHttpClientResponse(1);

            var characters = await characterClient.GetCharactersAsync("http://google.com");

            Assert.AreEqual(10, characters.Count);
        }

        private void ConfigureHttpClientResponse(int pages)
        {
            httpClient.Setup(x => x.Get(It.IsAny<HttpRequestMessage>())).ReturnsAsync((HttpRequestMessage request) =>
            {
                HttpResponseMessage response = new HttpResponseMessage();
                response.StatusCode = System.Net.HttpStatusCode.OK;
                response.Content = new StringContent(GetJsonCharacterResponse(pages));
                return response;
            });
        }

        private string GetJsonCharacterResponse(int page)
        {
            return "{\n\"info\": {\n \"count\": 10,\n \"pages\": *,\n \"next\": \"https://rickandmortyapi.com/api/character?page=2\",\n \"prev\": null\n},\n\"results\": [\n {\n \"id\": 1,\n \"name\": \"Rick Sanchez\",\n \"status\": \"Alive\",\n \"species\": \"Human\",\n \"type\": \"\",\n \"gender\": \"Male\",\n \"origin\": {\n     \"name\": \"Earth (C-137)\",\n     \"url\": \"https://rickandmortyapi.com/api/location/1\"\n },\n \"location\": {\n     \"name\": \"Earth (Replacement Dimension)\",\n     \"url\": \"https://rickandmortyapi.com/api/location/20\"\n },\n \"image\": \"https://rickandmortyapi.com/api/character/avatar/1.jpeg\",\n \"episode\": [\n     \"https://rickandmortyapi.com/api/episode/1\",\n     \"https://rickandmortyapi.com/api/episode/41\"\n ],\n \"url\": \"https://rickandmortyapi.com/api/character/1\",\n \"created\": \"2017-11-04T18:48:46.250Z\"\n },\n {\n \"id\": 2,\n \"name\": \"Morty Smith\",\n \"status\": \"Alive\",\n \"species\": \"Human\",\n \"type\": \"\",\n \"gender\": \"Male\",\n \"origin\": {\n     \"name\": \"Earth (C-137)\",\n     \"url\": \"https://rickandmortyapi.com/api/location/1\"\n },\n \"location\": {\n     \"name\": \"Earth (Replacement Dimension)\",\n     \"url\": \"https://rickandmortyapi.com/api/location/20\"\n },\n \"image\": \"https://rickandmortyapi.com/api/character/avatar/2.jpeg\",\n \"episode\": [\n     \"https://rickandmortyapi.com/api/episode/1\",\n     \"https://rickandmortyapi.com/api/episode/41\"\n ],\n \"url\": \"https://rickandmortyapi.com/api/character/2\",\n \"created\": \"2017-11-04T18:50:21.651Z\"\n },\n {\n \"id\": 3,\n \"name\": \"Summer Smith\",\n \"status\": \"Alive\",\n \"species\": \"Human\",\n \"type\": \"\",\n \"gender\": \"Female\",\n \"origin\": {\n     \"name\": \"Earth (Replacement Dimension)\",\n     \"url\": \"https://rickandmortyapi.com/api/location/20\"\n },\n \"location\": {\n     \"name\": \"Earth (Replacement Dimension)\",\n     \"url\": \"https://rickandmortyapi.com/api/location/20\"\n },\n \"image\": \"https://rickandmortyapi.com/api/character/avatar/3.jpeg\",\n \"episode\": [\n     \"https://rickandmortyapi.com/api/episode/6\",\n     \"https://rickandmortyapi.com/api/episode/41\"\n ],\n \"url\": \"https://rickandmortyapi.com/api/character/3\",\n \"created\": \"2017-11-04T19:09:56.428Z\"\n },\n {\n \"id\": 4,\n \"name\": \"Beth Smith\",\n \"status\": \"Alive\",\n \"species\": \"Human\",\n \"type\": \"\",\n \"gender\": \"Female\",\n \"origin\": {\n     \"name\": \"Earth (Replacement Dimension)\",\n     \"url\": \"https://rickandmortyapi.com/api/location/20\"\n },\n \"location\": {\n     \"name\": \"Earth (Replacement Dimension)\",\n     \"url\": \"https://rickandmortyapi.com/api/location/20\"\n },\n \"image\": \"https://rickandmortyapi.com/api/character/avatar/4.jpeg\",\n \"episode\": [\n     \"https://rickandmortyapi.com/api/episode/6\",\n     \"https://rickandmortyapi.com/api/episode/41\"\n ],\n \"url\": \"https://rickandmortyapi.com/api/character/4\",\n \"created\": \"2017-11-04T19:22:43.665Z\"\n },\n {\n \"id\": 5,\n \"name\": \"Jerry Smith\",\n \"status\": \"Alive\",\n \"species\": \"Human\",\n \"type\": \"\",\n \"gender\": \"Male\",\n \"origin\": {\n     \"name\": \"Earth (Replacement Dimension)\",\n     \"url\": \"https://rickandmortyapi.com/api/location/20\"\n },\n \"location\": {\n     \"name\": \"Earth (Replacement Dimension)\",\n     \"url\": \"https://rickandmortyapi.com/api/location/20\"\n },\n \"image\": \"https://rickandmortyapi.com/api/character/avatar/5.jpeg\",\n \"episode\": [\n     \"https://rickandmortyapi.com/api/episode/6\",\n     \"https://rickandmortyapi.com/api/episode/41\"\n ],\n \"url\": \"https://rickandmortyapi.com/api/character/5\",\n \"created\": \"2017-11-04T19:26:56.301Z\"\n },\n {\n \"id\": 6,\n \"name\": \"Abadango Cluster Princess\",\n \"status\": \"Alive\",\n \"species\": \"Alien\",\n \"type\": \"\",\n \"gender\": \"Female\",\n \"origin\": {\n     \"name\": \"Abadango\",\n     \"url\": \"https://rickandmortyapi.com/api/location/2\"\n },\n \"location\": {\n     \"name\": \"Abadango\",\n     \"url\": \"https://rickandmortyapi.com/api/location/2\"\n },\n \"image\": \"https://rickandmortyapi.com/api/character/avatar/6.jpeg\",\n \"episode\": [\n     \"https://rickandmortyapi.com/api/episode/27\"\n ],\n \"url\": \"https://rickandmortyapi.com/api/character/6\",\n \"created\": \"2017-11-04T19:50:28.250Z\"\n },\n {\n \"id\": 7,\n \"name\": \"Abradolf Lincler\",\n \"status\": \"unknown\",\n \"species\": \"Human\",\n \"type\": \"Genetic experiment\",\n \"gender\": \"Male\",\n \"origin\": {\n     \"name\": \"Earth (Replacement Dimension)\",\n     \"url\": \"https://rickandmortyapi.com/api/location/20\"\n },\n \"location\": {\n     \"name\": \"Testicle Monster Dimension\",\n     \"url\": \"https://rickandmortyapi.com/api/location/21\"\n },\n \"image\": \"https://rickandmortyapi.com/api/character/avatar/7.jpeg\",\n \"episode\": [\n     \"https://rickandmortyapi.com/api/episode/10\",\n     \"https://rickandmortyapi.com/api/episode/11\"\n ],\n \"url\": \"https://rickandmortyapi.com/api/character/7\",\n \"created\": \"2017-11-04T19:59:20.523Z\"\n },\n {\n \"id\": 8,\n \"name\": \"Adjudicator Rick\",\n \"status\": \"Dead\",\n \"species\": \"Human\",\n \"type\": \"\",\n \"gender\": \"Male\",\n \"origin\": {\n     \"name\": \"unknown\",\n     \"url\": \"\"\n },\n \"location\": {\n     \"name\": \"Citadel of Ricks\",\n     \"url\": \"https://rickandmortyapi.com/api/location/3\"\n },\n \"image\": \"https://rickandmortyapi.com/api/character/avatar/8.jpeg\",\n \"episode\": [\n     \"https://rickandmortyapi.com/api/episode/28\"\n ],\n \"url\": \"https://rickandmortyapi.com/api/character/8\",\n \"created\": \"2017-11-04T20:03:34.737Z\"\n },\n {\n \"id\": 9,\n \"name\": \"Agency Director\",\n \"status\": \"Dead\",\n \"species\": \"Human\",\n \"type\": \"\",\n \"gender\": \"Male\",\n \"origin\": {\n     \"name\": \"Earth (Replacement Dimension)\",\n     \"url\": \"https://rickandmortyapi.com/api/location/20\"\n },\n \"location\": {\n     \"name\": \"Earth (Replacement Dimension)\",\n     \"url\": \"https://rickandmortyapi.com/api/location/20\"\n },\n \"image\": \"https://rickandmortyapi.com/api/character/avatar/9.jpeg\",\n \"episode\": [\n     \"https://rickandmortyapi.com/api/episode/24\"\n ],\n \"url\": \"https://rickandmortyapi.com/api/character/9\",\n \"created\": \"2017-11-04T20:06:54.976Z\"\n },\n {\n \"id\": 10,\n \"name\": \"Alan Rails\",\n \"status\": \"Dead\",\n \"species\": \"Human\",\n \"type\": \"Superhuman (Ghost trains summoner)\",\n \"gender\": \"Male\",\n \"origin\": {\n     \"name\": \"unknown\",\n     \"url\": \"\"\n },\n \"location\": {\n     \"name\": \"Worldender's lair\",\n     \"url\": \"https://rickandmortyapi.com/api/location/4\"\n },\n \"image\": \"https://rickandmortyapi.com/api/character/avatar/10.jpeg\",\n \"episode\": [\n     \"https://rickandmortyapi.com/api/episode/25\"\n ],\n \"url\": \"https://rickandmortyapi.com/api/character/10\",\n \"created\": \"2017-11-04T20:19:09.017Z\"\n }\n]\n}".Replace("*", page.ToString());
        }
    }
}
