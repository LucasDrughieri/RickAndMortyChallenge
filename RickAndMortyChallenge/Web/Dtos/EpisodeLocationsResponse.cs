using System.Collections.Generic;

namespace Web.Dtos
{
    public class EpisodeLocationsResponse
    {
        public EpisodeLocationsResponse()
        {
            Episodes = new List<EpisodeLocationItem>();
        }

        public IList<EpisodeLocationItem> Episodes { get; set; }
        public string ProgramTimeElapsed { get; internal set; }
    }

    public class EpisodeLocationItem
    {
        public EpisodeLocationItem()
        {
            Locations = new List<LocationItem>();
            Origins = new List<LocationItem>();
        }

        public int Id { get; set; }

        public int TotalCharacters { get; set; }

        public IList<LocationItem> Locations { get; set; }

        public IList<LocationItem> Origins { get; set; }

        public string Episode { get; set; }
    }

    public class LocationItem
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
