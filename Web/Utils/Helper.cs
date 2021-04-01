using System.Text.RegularExpressions;

namespace Web.Utils
{
    public static class Helper
    {
        public static int GetLetterCount(string letter, string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return 0;

            return Regex.Matches(name.ToLowerInvariant(), letter.ToLowerInvariant()).Count;
        }
    }
}
