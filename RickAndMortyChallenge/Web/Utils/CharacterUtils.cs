using System;

namespace Web.Utils
{
    public static class CharacterUtils
    {
        private static readonly string CharacterUrl = "https://rickandmortyapi.com/api/character/";

        public static int GetIdByUrl(string url)
        {
            var idAsString = url.Replace(CharacterUrl, "");
            return Convert.ToInt32(idAsString);
        }
    }
}
