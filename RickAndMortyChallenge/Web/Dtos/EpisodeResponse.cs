using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Web.Dtos
{
    public class EpisodeResponse
    {
        public InfoResponse Info { get; set; }

        public IList<EpisodeResultResponse> Results { get; set; }
    }

    public class EpisodeResultResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Episode { get; set; }

        public IList<string> Characters { get; set; }

        public int GetLetterCount(string letter)
        {
            return Regex.Matches(Name.ToLowerInvariant(), letter.ToLowerInvariant()).Count;
        }
    }
}
