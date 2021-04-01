using RickAndMortyChallenge.ResponseModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Clients
{
    public class CharacterClient : BaseClient
    {
        public async Task<List<CharacterResultResponse>> GetCharactersAsync(string url)
        {
            var result = new List<CharacterResultResponse>();

            var characterResponse = await CallApiAsync<CharacterResponse>(url);

            result.AddRange(characterResponse.Results);

            var pageUrl = characterResponse.Info.Next;

            var runner = Task.Run(() => Parallel.For(2, characterResponse.Info.Pages, (index) =>
            {
                var newUrl = pageUrl.Replace("page=2", $"page={index}");

                var characterPageResponse = CallApiAsync<CharacterResponse>(newUrl).Result;

                lock (_lock)
                {
                    result.AddRange(characterPageResponse.Results);
                }
            }));

            runner.Wait();

            return result;
        }
    }
}
