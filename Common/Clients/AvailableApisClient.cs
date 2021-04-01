using RickAndMortyChallenge.ResponseModels;
using System.Threading.Tasks;

namespace Common.Clients
{
    public class AvailableApisClient : BaseClient
    {
        const string AvailableApisUrl = "https://rickandmortyapi.com/api";

        public async Task<AvailabeApisResponse> GetAvailableApisAsync()
        {
            return await CallApiAsync<AvailabeApisResponse>(AvailableApisUrl);
        }
    }
}
