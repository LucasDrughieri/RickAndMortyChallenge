using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Dtos;
using Web.Interfaces;
using Web.Resolvers;

namespace Tests.Resolvers
{
    public class CharacterResolverTests
    {
        private CharacterResolver characterResolver;
        private Mock<ICharacterClient> characterClient;

        [SetUp]
        public void Setup()
        {
            characterClient = new Mock<ICharacterClient>();

            var characterList = new List<CharacterResultResponse> {
                new CharacterResultResponse { Id = 1, Name = "Rick Sanchez", Location = new LocationItemResponse { Name = "Earth (Replacement Dimension)", Url = "https://rickandmortyapi.com/api/location/20" }, Origin = new LocationItemResponse { Name = "Earth (C-137)", Url = "https://rickandmortyapi.com/api/location/3" } },
                new CharacterResultResponse { Id = 2, Name = "Morty Smith", Location = new LocationItemResponse { Name = "Earth (Replacement Dimension)", Url = "https://rickandmortyapi.com/api/location/1" }, Origin = new LocationItemResponse { Name = "Earth (C-137)", Url = "https://rickandmortyapi.com/api/location/6" } },
                new CharacterResultResponse { Id = 3, Name = "Summer Smith", Location = new LocationItemResponse { Name = "Earth (Replacement Dimension)", Url = "https://rickandmortyapi.com/api/location/4" }, Origin = new LocationItemResponse { Name = "Earth (C-137)", Url = "https://rickandmortyapi.com/api/location/7" } },
                new CharacterResultResponse { Id = 4, Name = "Beth Smith", Location = new LocationItemResponse { Name = "Earth (Replacement Dimension)", Url = "https://rickandmortyapi.com/api/location/24" }, Origin = new LocationItemResponse { Name = "Earth (C-137)", Url = "https://rickandmortyapi.com/api/location/11" } },
                new CharacterResultResponse { Id = 5, Name = "Abadango Cluster Princess", Location = new LocationItemResponse { Name = "Earth (Replacement Dimension)", Url = "https://rickandmortyapi.com/api/location/8" }, Origin = new LocationItemResponse { Name = "Earth (C-137)", Url = "https://rickandmortyapi.com/api/location/12" } },
                new CharacterResultResponse { Id = 6, Name = "", Location = new LocationItemResponse { Name = "Earth (Replacement Dimension)", Url = "https://rickandmortyapi.com/api/location/8" }, Origin = new LocationItemResponse { Name = "Earth (C-137)", Url = "https://rickandmortyapi.com/api/location/12" } },
                new CharacterResultResponse { Id = 7, Name = null, Location = new LocationItemResponse { Name = "Earth (Replacement Dimension)", Url = "https://rickandmortyapi.com/api/location/8" }, Origin = new LocationItemResponse { Name = "Earth (C-137)", Url = "https://rickandmortyapi.com/api/location/12" } },
            };

            characterClient.Setup(x => x.GetCharactersAsync("url")).ReturnsAsync(characterList);
            characterClient.Setup(x => x.GetCharactersAsync("")).ReturnsAsync(new List<CharacterResultResponse>());
     
            characterResolver = new CharacterResolver(characterClient.Object);
        }

        [Test]
        public async Task MustRunSuccessfully()
        {
            var characterMessage = await characterResolver.Execute("url");

            Assert.AreEqual(characterMessage, "La letra c aparece 4 veces en los nombres de todos los personajes");
        }

        [Test]
        public async Task CharactersAreEmpty()
        {
            var characterMessage = await characterResolver.Execute("");

            Assert.AreEqual(characterMessage, "The characters API returns an empty list");
        }
    }
}
