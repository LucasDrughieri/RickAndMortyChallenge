using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Clients;
using Web.Dtos;
using Web.Utils;

namespace Web.Services
{
    public class EpisodeLocationsService
    {
        private readonly CharacterClient _characterClient;
        private readonly EpisodeClient _episodesClient;
        private readonly AvailableApisClient _availableApisClient;
        private readonly ILogger<EpisodeLocationsService> _logger;

        public EpisodeLocationsService(CharacterClient characterClient, EpisodeClient episodesClient, AvailableApisClient availableApisClient, ILogger<EpisodeLocationsService> logger)
        {
            _characterClient = characterClient;
            _episodesClient = episodesClient;
            _availableApisClient = availableApisClient;
            _logger = logger;
        }

        public async Task<EpisodeLocationsResponse> RunAsync()
        {
            IList<CharacterResultResponse> characters = new List<CharacterResultResponse>();
            IList<EpisodeResultResponse> episodes = new List<EpisodeResultResponse>();

            var availableApis = await _availableApisClient.GetAvailableApisAsync();

            var task1 = Task.Run(async () => {
                characters = await _characterClient.GetCharactersAsync(availableApis.Characters);
            });

            var task2 = Task.Run(async () => {
                episodes = await _episodesClient.GetEpisodesAsync(availableApis.Episodes);
            });

            Task.WaitAll(new[] { task1, task2 });

            var response = new EpisodeLocationsResponse();

            foreach (var episode in episodes)
            {
                var item = new EpisodeLocationItem();

                item.Id = episode.Id;
                item.Episode = episode.Episode;
                item.TotalCharacters = episode.Characters.Count;

                foreach (var characterUrl in episode.Characters)
                {
                    var id = CharacterUtils.GetIdByUrl(characterUrl);

                    var character = characters.FirstOrDefault(x => x.Id == id);

                    if (!string.IsNullOrWhiteSpace(character.Location.Url) && item.Locations.All(l => l.Id != character.Location.Id))
                    {
                        item.Locations.Add(new LocationItem { Id = character.Location.Id, Name = character.Location.Name });
                    }

                    if (!string.IsNullOrWhiteSpace(character.Origin.Url) && item.Origins.All(l => l.Id != character.Origin.Id))
                    {
                        item.Origins.Add(new LocationItem { Id = character.Origin.Id, Name = character.Origin.Name });
                    }
                }

                response.Episodes.Add(item);
            }

            return response;
        }
    }
}
