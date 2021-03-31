using System.Linq;
using System.Threading.Tasks;
using Web.Clients;

namespace Web.Resolvers
{
    /// <summary>
    /// Class to resolve how many the letter c (case insensitive) appears in all characters´s name
    /// </summary>
    public class CharacterResolver
    {
        const string letter = "c";

        private CharacterClient characterClient;

        public CharacterResolver(CharacterClient _characterClient)
        {
            characterClient = _characterClient;
        }

        public async Task<string> Execute(string charactersUrl)
        {
            //Calling characterClient to get all characters by http get
            var characters = await characterClient.GetCharactersAsync(charactersUrl);

            if (!characters.Any()) return "The characters API returns an empty list";

            return $"La letra {letter} aparece {characters.Sum(_ => _.GetLetterCount(letter))} veces en los nombres de todos los personajes";
        }
    }
}
