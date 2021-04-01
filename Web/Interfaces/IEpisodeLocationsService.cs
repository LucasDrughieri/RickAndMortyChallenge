using System.Threading.Tasks;
using Web.Dtos;

namespace Web.Interfaces
{
    public interface IEpisodeLocationsService
    {
        Task<EpisodeLocationsResponse> RunAsync();
    }
}
