using CharCounter.Tasks;
using Common.Utils;
using RickAndMorty.Net.Api.Factory;
using RickAndMortyChallenge.Client;
using System;
using System.Threading.Tasks;

namespace CharCounter
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var availableApisClient = new AvailableApisClient();

            var service = RickAndMortyApiFactory.Create();

            var timer = new TimerDecorator();

            var availableApis = await availableApisClient.GetAvailableApisAsync();

            timer.Iniciar();
            await new CharacterTask().Execute(service, availableApis.Characters);
            var elapsed = timer.Detener();

            Console.WriteLine($"tiempo total {elapsed}");

            Console.ReadKey();
        }
    }
}
