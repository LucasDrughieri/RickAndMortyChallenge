using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Clients;
using Web.Dtos;
using Web.Resolvers;

namespace Tests.Resolvers
{
    public class CharacterResolverTests
    {
        private CharacterResolver characterResolver;
        private Mock<CharacterClient> characterClient;

        [SetUp]
        public void Setup()
        {
            characterClient = new Mock<CharacterClient>();

            var characterList = new List<CharacterResultResponse> {
                new CharacterResultResponse { Id = 1, Name = "Rick Sanchez", Location = new LocationItemResponse { Name = "Earth (Replacement Dimension)", Url = "https://rickandmortyapi.com/api/location/20" }, Origin = new LocationItemResponse { Name = "Earth (C-137)", Url = "https://rickandmortyapi.com/api/location/3" } },
                new CharacterResultResponse { Id = 2, Name = "Morty Smith", Location = new LocationItemResponse { Name = "Earth (Replacement Dimension)", Url = "https://rickandmortyapi.com/api/location/1" }, Origin = new LocationItemResponse { Name = "Earth (C-137)", Url = "https://rickandmortyapi.com/api/location/6" } },
                new CharacterResultResponse { Id = 3, Name = "Summer Smith", Location = new LocationItemResponse { Name = "Earth (Replacement Dimension)", Url = "https://rickandmortyapi.com/api/location/4" }, Origin = new LocationItemResponse { Name = "Earth (C-137)", Url = "https://rickandmortyapi.com/api/location/7" } },
                new CharacterResultResponse { Id = 4, Name = "Beth Smith", Location = new LocationItemResponse { Name = "Earth (Replacement Dimension)", Url = "https://rickandmortyapi.com/api/location/24" }, Origin = new LocationItemResponse { Name = "Earth (C-137)", Url = "https://rickandmortyapi.com/api/location/11" } },
                new CharacterResultResponse { Id = 5, Name = "Abadango Cluster Princess", Location = new LocationItemResponse { Name = "Earth (Replacement Dimension)", Url = "https://rickandmortyapi.com/api/location/8" }, Origin = new LocationItemResponse { Name = "Earth (C-137)", Url = "https://rickandmortyapi.com/api/location/12" } },
            };

            characterClient.Setup(x => x.GetCharactersAsync(It.IsAny<string>())).Returns(Task.FromResult(characterList));
     
            characterResolver = new CharacterResolver(characterClient.Object);
        }

        [Test]
        public void Test1()
        {
            var characterMessage = characterResolver.Execute("");

            Assert.Equals(characterMessage, "La letra c aparece 4 veces en los nombres de todos los personajes");
        }
    }
}
