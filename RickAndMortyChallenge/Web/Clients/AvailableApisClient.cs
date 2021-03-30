using System.Net.Http;
using System.Threading.Tasks;
using Web.Dtos;

namespace Web.Clients
{
    public class AvailableApisClient : BaseClient
    {
        const string AvailableApisUrl = "https://rickandmortyapi.com/api";

        public AvailableApisClient(HttpClient client) : base(client)
        {
        }

        public async Task<AvailabeApisResponse> GetAvailableApisAsync()
        {
            return await CallApiAsync<AvailabeApisResponse>(AvailableApisUrl);
        }
    }
}
