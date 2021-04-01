using System.Collections.Generic;

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
    }
}
