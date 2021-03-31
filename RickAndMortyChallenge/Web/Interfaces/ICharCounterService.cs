using System.Threading.Tasks;
using Web.Dtos;

namespace Web.Interfaces
{
    public interface ICharCounterService
    {
        Task<CharCounterResponse> RunAsync();
    }
}
