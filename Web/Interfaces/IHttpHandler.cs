using System.Net.Http;
using System.Threading.Tasks;

namespace Web.Interfaces
{
    public interface IHttpHandler
    {
        Task<HttpResponseMessage> Get(HttpRequestMessage httpRequestMessage);
    }
}
