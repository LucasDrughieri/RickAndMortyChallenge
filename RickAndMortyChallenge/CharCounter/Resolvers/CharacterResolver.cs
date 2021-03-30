using Common.Clients;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CharCounter.Resolvers
{
    public class CharacterResolver
    {
        const string letter = "c";

        private CharacterClient characterClient;

        public CharacterResolver(CharacterClient _characterClient)
        {
            characterClient = _characterClient;
        }

        public async Task Execute(string charactersUrl)
        {
            var characters = await characterClient.GetCharactersAsync(charactersUrl);

            Console.WriteLine($"La letra {letter} aparece {characters.Sum(_ => _.GetLetterCount(letter))} veces en los nombres de todos los personajes");
        }
    }
}
