using System.Threading.Tasks;

namespace Web.Interfaces
{
    public interface IBaseClient
    {
        Task<T> CallGetApiAsync<T>(string url) where T : class;
    }
}
