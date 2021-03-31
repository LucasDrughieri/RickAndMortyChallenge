﻿using System.Linq;
using System.Threading.Tasks;
using Web.Clients;
using Web.Interfaces;

namespace Web.Resolvers
{
    /// <summary>
    /// Class to resolve how many the letter I (case insensitive) appears in all location´s name
    /// </summary>
    public class LocationResolver : ILocationResolver
    {
        const string letter = "I";

        private ILocationClient locationClient;

        public LocationResolver(ILocationClient _locationClient)
        {
            locationClient = _locationClient;
        }

        public async Task<string> Execute(string locationUrl)
        {
            //Calling locationClient to get all locations by http get
            var locations = await locationClient.GetLocationsAsync(locationUrl);

            if (!locations.Any()) return "The episodes API returns an empty list";

            return $"La letra {letter} aparece {locations.Sum(_ => _.GetLetterCount(letter))} veces en los nombres de todas las ubicaciones";
        }
    }
}
