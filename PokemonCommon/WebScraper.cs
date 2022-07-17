using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using HtmlAgilityPack;
using System.Drawing;

namespace PokemonCommon
{
    public class WebScraper : IPokemonDataLayer
    {
        public const string POKEMONDB_DETAILS_URL = "http://pokemondb.net/pokedex/";
       
        private IDictionary<int, string> numberToName;

        public WebScraper()
        {
            numberToName = new Dictionary<int, string>();
            
            using (StreamReader sr = new StreamReader(File.OpenRead("..\\..\\PokemonMap.txt")))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    string[] parts = line.Split(',');
                    int pokemonNumber = int.Parse(parts[0]);
                    string pokemonName = parts[1];
                    if (!numberToName.ContainsKey(pokemonNumber))
                    {
                        numberToName.Add(pokemonNumber, pokemonName);
                    }
                }
            }
        }

        public Pokemon GetPokemonByNumber(int pokemonNumber)
        {
            Pokemon pokemon = new Pokemon();
            
            pokemon.Number = pokemonNumber;
            pokemon.Name = numberToName[pokemonNumber];

            WebClient client = new WebClient();

            string urlFriendlyName = pokemon.Name.ToLower();
            urlFriendlyName = urlFriendlyName.Replace(". ", "-").Replace(".", "").Replace("'", "").Replace(" ", "");
            string body = client.DownloadString(POKEMONDB_DETAILS_URL + urlFriendlyName);

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(body);

            int rowNumber = 0;

            foreach (HtmlNode row in doc.DocumentNode.SelectNodes("//table[@class='vitals-table']/tbody/tr"))
            {
                ++rowNumber;
                HtmlNode td = row.Element("td");

                switch (rowNumber)
                {
                    case 2:
                        IEnumerable<HtmlNode> anchors = td.Elements("a");
                        List<PokemonType> typeList = new List<PokemonType>();
                        foreach (HtmlNode a in anchors)
                        {
                            typeList.Add((PokemonType)Enum.Parse(typeof(PokemonType), a.InnerText));
                        }
                        pokemon.Types = typeList.ToArray();
                        break;

                    case 3:
                        pokemon.Species = td.InnerText;
                        break;

                    case 4:
                        pokemon.Height = td.InnerText;
                        break;

                    case 5:
                        pokemon.Weight = td.InnerText;
                        break;

                    case 6:
                        anchors = td.Elements("a");
                        List<string> abilityList = new List<string>();
                        foreach (HtmlNode a in anchors)
                        {
                            abilityList.Add(a.InnerText);
                        }
                        pokemon.Abilities = abilityList.ToArray();
                        break;
                }
            }

            HtmlNode imageNode = doc.DocumentNode.SelectSingleNode("//div[@class='col desk-span-4 lap-span-6 figure']/img");
            string imageUrl = imageNode.GetAttributeValue("src", "null");
            var request = WebRequest.Create(imageUrl);
            using (var response = request.GetResponse())
            {
                using (var stream = response.GetResponseStream())
                {
                    //pokemon.Picture = Bitmap.FromStream(stream);
                }
            }

            return pokemon;
        }

        public int GetNumberOfPokemon()
        {
            return numberToName.Count;
        }
    }
}
