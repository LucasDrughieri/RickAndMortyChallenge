using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Dtos;

namespace Web.Interfaces
{
    public interface ICharacterClient : IBaseClient
    {
        Task<List<CharacterResultResponse>> GetCharactersAsync(string url);
    }
}
