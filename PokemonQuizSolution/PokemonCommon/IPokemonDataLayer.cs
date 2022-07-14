using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonCommon
{
    public interface IPokemonDataLayer
    {
        Pokemon GetPokemonByNumber(int pokemonNumber);
        int GetNumberOfPokemon();
    }
}
