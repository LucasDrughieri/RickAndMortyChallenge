using RickAndMortyChallenge.ResponseModels;
using System.Text.RegularExpressions;

namespace Common.ResponseModels
{
    public class LocationResponse
    {
        public InfoResponse Info { get; set; }
    }

    public class LocationResultResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int GetLetterCount(string letter)
        {
            return Regex.Matches(Name.ToLowerInvariant(), letter.ToLowerInvariant()).Count;
        }
    }
}
