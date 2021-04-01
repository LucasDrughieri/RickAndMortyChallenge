using System;

namespace Web.Utils
{
    public static class CharacterUtils
    {
        public static int GetIdByUrl(string url, string charactersBaseUrl)
        {
            var idAsString = url.Replace($"{charactersBaseUrl}/", "");
            return Convert.ToInt32(idAsString);
        }
    }
}
