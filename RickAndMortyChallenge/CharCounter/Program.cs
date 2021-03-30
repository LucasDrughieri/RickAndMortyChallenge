using CharCounter.Resolvers;
using Common.Clients;
using Common.Utils;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace CharCounter
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var serviceProvider = Configuration.SetupDI();

            var availableApisClient = serviceProvider.GetService<AvailableApisClient>();

            var timer = new TimerDecorator();

            timer.Start();

            var availableApis = await availableApisClient.GetAvailableApisAsync();

            var task1 = Task.Run(async () => {
                var characterTask = serviceProvider.GetService<CharacterResolver>();
                await characterTask.Execute(availableApis.Characters);
            });

            var task2 = Task.Run(async () => {
                var locationTask = serviceProvider.GetService<LocationResolver>();
                await locationTask.Execute(availableApis.Locations);
            });

            var task3 = Task.Run(async () => {
                var episodeTask = serviceProvider.GetService<EpisodesResolver>();
                await episodeTask.Execute(availableApis.Episodes);
            });

            Task.WaitAll(new[] { task1, task2, task3 });

            Console.WriteLine(asd);

            var elapsed = timer.Stop();

            Console.WriteLine($"El tiempo total del programa fue {elapsed}");

            Console.ReadKey();
        }
    }
}
