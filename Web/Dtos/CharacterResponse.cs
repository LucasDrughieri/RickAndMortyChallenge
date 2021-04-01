using System.Collections.Generic;

namespace Web.Dtos
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

        public LocationItemResponse Origin { get; set; }

        public LocationItemResponse Location { get; set; }
    }
}
