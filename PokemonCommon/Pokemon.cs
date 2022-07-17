using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PokemonCommon
{
    public class Pokemon
    {
        public string Name { get; set; }
        public int Number { get; set; }
        public PokemonType[] Types { get; set; }
        public string Species { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public string[] Abilities { get; set; }
        public byte[] Picture { get; set; }
    }
}
