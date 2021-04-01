using Common.Clients;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CharCounter.Resolvers
{
    public class LocationResolver
    {
        const string letter = "I";

        private LocationClient locationClient;

        public LocationResolver(LocationClient _locationClient)
        {
            locationClient = _locationClient;
        }

        public async Task Execute(string charactersUrl)
        {
            var characters = await locationClient.GetLocationsAsync(charactersUrl);

            Console.WriteLine($"La letra {letter} aparece {characters.Sum(_ => _.GetLetterCount(letter))} veces en los nombres de todas las ubicaciones");
        }
    }
}
