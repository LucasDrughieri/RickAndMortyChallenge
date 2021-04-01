using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Web.Dtos
{
    public class LocationResponse
    {
        public InfoResponse Info { get; set; }

        public IList<LocationResultResponse> Results { get; set; }
    }

    public class LocationResultResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int GetLetterCount(string letter)
        {
            if (string.IsNullOrWhiteSpace(Name)) return 0;

            return Regex.Matches(Name.ToLowerInvariant(), letter.ToLowerInvariant()).Count;
        }
    }
}
