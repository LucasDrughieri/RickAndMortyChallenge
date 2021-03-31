using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.Dtos;
using Web.Interfaces;
using Web.Services;
using Web.Utils;

namespace Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EpisodeLocationController : ControllerBase
    {
        private readonly IEpisodeLocationsService _episodeLocationsService;

        public EpisodeLocationController(IEpisodeLocationsService episodeLocationsService)
        {
            _episodeLocationsService = episodeLocationsService;
        }

        /// <summary>
        /// Endpoint to resolve excercise 2 (Episode Location)
        /// </summary>
        [HttpGet]
        public async Task<EpisodeLocationsResponse> ResolveEpisodeLocationExcercise()
        {
            var timer = new TimerDecorator();

            timer.Start();

            var response = await _episodeLocationsService.RunAsync();

            var elapsed = timer.Stop();

            response.ProgramTimeElapsed = $"El tiempo total del programa fue {elapsed}";

            return response;
        }
    }
}
