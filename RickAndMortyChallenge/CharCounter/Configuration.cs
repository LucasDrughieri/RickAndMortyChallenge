using CharCounter.Tasks;
using Common.Clients;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CharCounter
{
    public static class Configuration
    {
        public static IServiceProvider SetupDI()
        {
            var serviceProvider = new ServiceCollection()
                .AddScoped<CharacterTask>()
                .AddScoped<LocationTask>()
                .AddScoped<EpisodesTask>()

                .AddScoped<CharacterClient>()
                .AddScoped<LocationClient>()
                .AddScoped<EpisodeClient>()

                .AddScoped<AvailableApisClient>()

                .BuildServiceProvider();

            return serviceProvider;
        }
    }
}
