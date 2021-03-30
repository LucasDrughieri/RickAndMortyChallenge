using System.Threading.Tasks;
using Web.Clients;
using Web.Dtos;
using Web.Resolvers;

namespace Web.Services
{
    public class CharCounterService
    {
        private readonly CharacterResolver _characterTask; 
        private readonly LocationResolver _locationResolver; 
        private readonly EpisodesResolver _episodesResolver;
        private readonly AvailableApisClient _availableApisClient;

        public CharCounterService(CharacterResolver characterTask, LocationResolver locationResolver, EpisodesResolver episodesResolver, AvailableApisClient availableApisClient)
        {
            _characterTask = characterTask;
            _locationResolver = locationResolver;
            _episodesResolver = episodesResolver;
            _availableApisClient = availableApisClient;
        }

        public async Task<CharCounterResponse> RunAsync()
        {
            var response = new CharCounterResponse();

            var availableApis = await _availableApisClient.GetAvailableApisAsync();

            var task1 = Task.Run(async () => {
                response.CharacterMessage = await _characterTask.Execute(availableApis.Characters);
            });

            var task2 = Task.Run(async () => {
                response.LocationMessage = await _locationResolver.Execute(availableApis.Locations);
            });

            var task3 = Task.Run(async () => {
                response.EpisodeMessage = await _episodesResolver.Execute(availableApis.Episodes);
            });

            Task.WaitAll(new[] { task1, task2, task3 });

            return response;
        }
    }
}
