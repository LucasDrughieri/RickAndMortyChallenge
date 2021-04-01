using System.Threading.Tasks;

namespace Web.Interfaces
{
    public interface IEpisodesResolver
    {
        Task<string> Execute(string episodesUrl);
    }
}
