using Common.Utils;
using RickAndMorty.Net.Api.Service;
using RickAndMortyChallenge.Client;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CharCounter.Tasks
{
    public class CharacterTask
    {
        const string letter = "c";

        private CharacterClient characterClient;

        public CharacterTask()
        {
            characterClient = new CharacterClient();
        }

        public async Task Execute(IRickAndMortyService service, string charactersUrl)
        {
            var timer = new TimerDecorator();

            timer.Iniciar();
            var characters = await characterClient.GetCharactersAsync(charactersUrl);
            var elapsed = timer.Detener();

            Console.WriteLine($"tiempo de las apis {elapsed}");

            Console.WriteLine($"La letra c aparece {characters.Sum(_ => _.GetLetterCount(letter))} veces en los nombres de todos personajes");
        }
    }
}
