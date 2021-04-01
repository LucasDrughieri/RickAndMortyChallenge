using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net.Http;
using Web.Clients;
using Web.Interfaces;
using Web.Middleware;
using Web.Resolvers;
using Web.Services;

namespace Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            //Configuración de DI
            services.AddSingleton<HttpClient>();

            services.AddTransient<ICharacterResolver, CharacterResolver>();
            services.AddTransient<ILocationResolver, LocationResolver>();
            services.AddTransient<IEpisodesResolver, EpisodesResolver>();

            services.AddTransient<ICharCounterService, CharCounterService>();
            services.AddTransient<IEpisodeLocationsService, EpisodeLocationsService>();

            services.AddTransient<ICharacterClient, CharacterClient>();
            services.AddTransient<ILocationClient, LocationClient>();
            services.AddTransient<IEpisodeClient, EpisodeClient>();
            services.AddTransient<IAvailableApisClient, AvailableApisClient>();

            services.AddScoped<IHttpHandler, Utils.HttpClientHandler>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
