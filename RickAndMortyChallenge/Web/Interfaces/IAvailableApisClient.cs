using System.Threading.Tasks;
using Web.Dtos;

namespace Web.Interfaces
{
    public interface IAvailableApisClient : IBaseClient
    {
        Task<AvailabeApisResponse> GetAvailableApisAsync();
    }
}
