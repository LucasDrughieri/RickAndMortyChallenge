using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Dtos;

namespace Web.Interfaces
{
    public interface ILocationClient : IBaseClient
    {
        Task<List<LocationResultResponse>> GetLocationsAsync(string url);
    }
}
