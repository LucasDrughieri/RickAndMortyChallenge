using System.Threading.Tasks;

namespace Web.Interfaces
{
    public interface ICharacterResolver
    {
        Task<string> Execute(string charactersUrl);
    }
}
