using Common.Clients;
using Common.Utils;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CharCounter.Tasks
{
    public class CharacterTask
    {
        const string letter = "c";

        private CharacterClient characterClient;

        public CharacterTask(CharacterClient _characterClient)
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
