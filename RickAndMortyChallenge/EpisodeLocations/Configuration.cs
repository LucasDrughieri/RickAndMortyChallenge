using CharCounter.Resolvers;
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
                .AddScoped<CharacterResolver>()
                .AddScoped<LocationResolver>()
                .AddScoped<EpisodesResolver>()

                .AddScoped<CharacterClient>()
                .AddScoped<LocationClient>()
                .AddScoped<EpisodeClient>()

                .AddScoped<AvailableApisClient>()

                .BuildServiceProvider();

            return serviceProvider;
        }
    }
}
