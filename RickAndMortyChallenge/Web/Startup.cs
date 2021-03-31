using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Web.Clients;
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
            services.AddScoped<CharacterResolver>();
            services.AddScoped<LocationResolver>();
            services.AddScoped<EpisodesResolver>();

            services.AddScoped<CharCounterService>();
            services.AddScoped<EpisodeLocationsService>();

            services.AddHttpClient<CharacterClient>();
            services.AddHttpClient<LocationClient>();
            services.AddHttpClient<EpisodeClient>();
            services.AddHttpClient<AvailableApisClient>();
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
