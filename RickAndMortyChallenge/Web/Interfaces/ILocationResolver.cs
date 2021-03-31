using System.Threading.Tasks;

namespace Web.Interfaces
{
    public interface ILocationResolver
    {
        Task<string> Execute(string locationUrl);
    }
}
