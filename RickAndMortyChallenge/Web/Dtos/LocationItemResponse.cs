using System;

namespace Web.Dtos
{
    public class LocationItemResponse
    {
        private readonly string LocationUrl = "https://rickandmortyapi.com/api/location/";

        public string Name { get; set; }

        public string Url { get; set; }

        public int Id
        {
            get
            {
                var idAsString = Url.Replace(LocationUrl, "");
                return Convert.ToInt32(idAsString);
            }
        }
    }
}
