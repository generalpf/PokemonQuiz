using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Db4objects.Db4o;

namespace PokemonCommon
{
    public class Db4oDataLayer : IPokemonDataLayer
    {
        private IObjectContainer db = null;

        public Db4oDataLayer()
        {
            db = Db4oEmbedded.OpenFile("..\\..\\pokemon.db4o");
        }

        ~Db4oDataLayer()
        {
            if (db != null)
            {
                db.Close();
            }
        }

        public Pokemon GetPokemonByNumber(int pokemonNumber)
        {
            IList<Pokemon> results = db.Query<Pokemon>(delegate(Pokemon pokemon) {
                return pokemon.Number == pokemonNumber;
            });
            return results[0];
        }

        public int GetNumberOfPokemon()
        {
            return 718;
        }
      
    }
}
