using System;
using System.Linq;
using System.Threading.Tasks;
using Web.Clients;

namespace Web.Resolvers
{
    public class LocationResolver
    {
        const string letter = "I";

        private LocationClient locationClient;

        public LocationResolver(LocationClient _locationClient)
        {
            locationClient = _locationClient;
        }

        public async Task<string> Execute(string charactersUrl)
        {
            var characters = await locationClient.GetLocationsAsync(charactersUrl);

            return $"La letra {letter} aparece {characters.Sum(_ => _.GetLetterCount(letter))} veces en los nombres de todas las ubicaciones";
        }
    }
}
