using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Dtos;
using Web.Interfaces;
using Web.Utils;

namespace Web.Services
{
    public class EpisodeLocationsService : IEpisodeLocationsService
    {
        private readonly ICharacterClient _characterClient;
        private readonly IEpisodeClient _episodesClient;
        private readonly IAvailableApisClient _availableApisClient;

        public EpisodeLocationsService(ICharacterClient characterClient, IEpisodeClient episodesClient, IAvailableApisClient availableApisClient)
        {
            _characterClient = characterClient;
            _episodesClient = episodesClient;
            _availableApisClient = availableApisClient;
        }

        public async Task<EpisodeLocationsResponse> RunAsync()
        {
            IList<CharacterResultResponse> characters = new List<CharacterResultResponse>();
            IList<EpisodeResultResponse> episodes = new List<EpisodeResultResponse>();

            // Get available API's resources
            var availableApis = await _availableApisClient.GetAvailableApisAsync();

            //Call to _characterClient to get all characters
            var charactersTask = Task.Run(async () =>
            {
                characters = await _characterClient.GetCharactersAsync(availableApis.Characters);

                if (!characters.Any()) throw new AppException("The characters API returns an empty list");
            });

            //Call to _episodesClient to get all episodes
            var episodesTask = Task.Run(async () =>
            {
                episodes = await _episodesClient.GetEpisodesAsync(availableApis.Episodes);

                if (!episodes.Any()) throw new AppException("The episodes API returns an empty list");
            });

            //Waiting until all tasks finish
            Task.WaitAll(new[] { charactersTask, episodesTask });

            var response = new EpisodeLocationsResponse();

            //Loop over all episodes
            foreach (var episode in episodes)
            {
                var item = new EpisodeLocationItem();

                item.Id = episode.Id;
                item.Episode = episode.Episode;
                item.TotalCharacters = episode.Characters.Count;

                //Loop over all characters in an episode
                foreach (var characterUrl in episode.Characters)
                {
                    //Get character id by characterUrl
                    var id = CharacterUtils.GetIdByUrl(characterUrl, availableApis.Characters);

                    var character = characters.FirstOrDefault(x => x.Id == id);

                    if (character == null) continue;

                    //If character location exists and if it is not in item.Locations, it will be added to the list
                    SetLocation(availableApis, item, character);

                    //If character origin exists and if it is not in item.Origins, it will be added to the list
                    SetOrigin(availableApis, item, character);
                }

                response.Episodes.Add(item);
            }

            return response;
        }

        private void SetOrigin(AvailabeApisResponse availableApis, EpisodeLocationItem item, CharacterResultResponse character)
        {
            var originId = character.Origin.GetId(availableApis.Locations);

            if (originId != null && item.Origins.All(l => l.Id != originId))
            {
                item.Origins.Add(new LocationItem { Id = originId.Value, Name = character.Location.Name });
            }
        }

        private void SetLocation(AvailabeApisResponse availableApis, EpisodeLocationItem item, CharacterResultResponse character)
        {
            var locationId = character.Location.GetId(availableApis.Locations);

            if (locationId != null && item.Locations.All(l => l.Id != locationId))
            {
                item.Locations.Add(new LocationItem { Id = locationId.Value, Name = character.Location.Name });
            }
        }
    }
}
