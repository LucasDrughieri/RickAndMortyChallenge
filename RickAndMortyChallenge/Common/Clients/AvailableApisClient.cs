using Newtonsoft.Json;
using RickAndMortyChallenge.ResponseModels;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Common.Clients
{
    public class AvailableApisClient
    {
        const string AvailableApisUrl = "https://rickandmortyapi.com/api";

        public async Task<AvailabeApisResponse> GetAvailableApisAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                using (HttpResponseMessage response = await client.GetAsync(AvailableApisUrl))
                {
                    response.EnsureSuccessStatusCode();

                    string responseBody = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<AvailabeApisResponse>(responseBody);
                }
            }
        }
    }
}
