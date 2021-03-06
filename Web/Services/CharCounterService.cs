using System.Threading.Tasks;
using Web.Clients;
using Web.Dtos;
using Web.Interfaces;
using Web.Resolvers;

namespace Web.Services
{
    public class CharCounterService : ICharCounterService
    {
        private readonly ICharacterResolver _characterResolver; 
        private readonly ILocationResolver _locationResolver; 
        private readonly IEpisodesResolver _episodesResolver;
        private readonly IAvailableApisClient _availableApisClient;

        public CharCounterService(ICharacterResolver characterTask, ILocationResolver locationResolver, IEpisodesResolver episodesResolver, IAvailableApisClient availableApisClient)
        {
            _characterResolver = characterTask;
            _locationResolver = locationResolver;
            _episodesResolver = episodesResolver;
            _availableApisClient = availableApisClient;
        }

        /// <summary>
        /// Char counter excercise implementation
        /// </summary>
        public async Task<CharCounterResponse> RunAsync()
        {
            var response = new CharCounterResponse();

            // Get available API's resources
            var availableApis = await _availableApisClient.GetAvailableApisAsync();

            //Call to character resolver to calculate how many the letter c appears in all characters´s name
            var characterTask = Task.Run(async () => {
                response.CharacterMessage = await _characterResolver.Execute(availableApis.Characters);
            });

            //Call to location resolver to calculate how many the letter I appears in all location´s name
            var locationTask = Task.Run(async () => {
                response.LocationMessage = await _locationResolver.Execute(availableApis.Locations);
            });

            //Call to episode resolver to calculate how many the letter e appears in all episodes´s name
            var episodeTask = Task.Run(async () => {
                response.EpisodeMessage = await _episodesResolver.Execute(availableApis.Episodes);
            });

            //Waiting until all tasks finish
            Task.WaitAll(new[] { characterTask, locationTask, episodeTask });

            return response;
        }
    }
}
