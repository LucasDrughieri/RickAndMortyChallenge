using RickAndMortyChallenge.ResponseModels;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Text.Json;

namespace RickAndMortyChallenge.Client
{
    public class CharacterClient
    {
        const string characterPageUrl = "https://rickandmortyapi.com/api/character/?page=";
        static object _lock = new object();

        public async Task<List<CharacterResultResponse>> GetCharactersAsync(string url)
        {
            var result = new List<CharacterResultResponse>();

            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var characterResponse = await CallApiAsync(client, url);

            result.AddRange(characterResponse.Results);

            var runner = Task.Run(() => Parallel.For(2, characterResponse.Info.Pages, (index) =>
            {
                characterResponse = CallApiAsync(client, string.Concat(characterPageUrl, index)).Result;

                lock (_lock)
                {
                    result.AddRange(characterResponse.Results);
                }
            }));

            runner.Wait();

            return result;
        }

        private async Task<CharacterResponse> CallApiAsync(HttpClient client, string url)
        {
            using (HttpRequestMessage httpRequestMessage = new HttpRequestMessage())
            {
                httpRequestMessage.RequestUri = new System.Uri(url);
                httpRequestMessage.Method = HttpMethod.Get;

                using (HttpResponseMessage response = await client.SendAsync(httpRequestMessage))
                {
                    string responseBody = await response.Content.ReadAsStringAsync();

                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                    };

                    var charactersData = JsonSerializer.Deserialize<CharacterResponse>(responseBody, options);

                    return charactersData;
                }
            }
        }
    }
}
