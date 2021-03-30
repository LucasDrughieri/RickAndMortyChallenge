using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Web.Dtos;
using Web.Services;
using Web.Utils;

namespace Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EpisodeLocationController : ControllerBase
    {
        private readonly EpisodeLocationsService _episodeLocationsService;

        public EpisodeLocationController(EpisodeLocationsService episodeLocationsService)
        {
            _episodeLocationsService = episodeLocationsService;
        }

        [HttpGet]
        public async Task<EpisodeLocationsResponse> GetAsync()
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
