using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Web.Dtos;
using Web.Interfaces;
using Web.Services;
using Web.Utils;

namespace Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CharCounterController : ControllerBase
    {
        private readonly ILogger<CharCounterController> _logger;
        private readonly ICharCounterService _charCounterService;

        public CharCounterController(ILogger<CharCounterController> logger, ICharCounterService charCounterService)
        {
            _logger = logger;
            _charCounterService = charCounterService;
        }

        /// <summary>
        /// Endpoint to resolve excercie 1 (Char Counter)
        /// </summary>
        [HttpGet]
        public async Task<CharCounterResponse> ResolveCharCounterExcercise()
        {
            var timer = new TimerDecorator();

            timer.Start();

            var response = await _charCounterService.RunAsync();

            var elapsed = timer.Stop();

            response.ProgramTimeElapsed = $"El tiempo total del programa fue {elapsed}";

            return response;
        }
    }
}
