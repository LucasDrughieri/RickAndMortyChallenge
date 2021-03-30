using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RickAndMortyChallenge.ResponseModels
{
    public class CharacterResponse
    {
        public InfoResponse Info { get; set; }

        public IList<CharacterResultResponse> Results { get; set; }
    }

    public class CharacterResultResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int GetLetterCount(string letter)
        {
            return Regex.Matches(Name.ToLowerInvariant(), letter.ToLowerInvariant()).Count;
        }
    }
}
