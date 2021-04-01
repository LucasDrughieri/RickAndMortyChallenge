using System;

namespace Web.Dtos
{
    public class LocationItemResponse
    {
        public string Name { get; set; }

        public string Url { get; set; }

        public int? GetId(string locationBaseUrl)
        {
            if(!string.IsNullOrWhiteSpace(Url) && Url.Contains(locationBaseUrl))
            {
                var idAsString = Url.Replace($"{locationBaseUrl}/", "");
                return Convert.ToInt32(idAsString);
            }

            return null;
        }
    }
}
