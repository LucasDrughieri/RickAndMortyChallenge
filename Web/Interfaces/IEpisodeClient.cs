using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Dtos;

namespace Web.Interfaces
{
    public interface IEpisodeClient : IBaseClient
    {
        Task<List<EpisodeResultResponse>> GetEpisodesAsync(string url);
    }
}
