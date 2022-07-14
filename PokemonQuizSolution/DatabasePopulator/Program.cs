using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Drawing;
using HtmlAgilityPack;
using PokemonCommon;
using Db4objects.Db4o;

namespace DatabasePopulator
{
    class Program
    {
        private const string POKEMONDB_URL = "http://pokemondb.net/pokedex/all";

        static void Main(string[] args)
        {
            IEmbeddedObjectContainer db = null;

            try
            {
                Db4objects.Db4o.Config.IEmbeddedConfiguration config = Db4oEmbedded.NewConfiguration();
                config.Common.ObjectClass(typeof(Pokemon)).Indexed(true);
                db = Db4oEmbedded.OpenFile(config, "..\\..\\pokemon.db4o");

                WebClient client = new WebClient();

                string allBody = client.DownloadString(POKEMONDB_URL);
                HtmlDocument allDoc = new HtmlDocument();
                allDoc.LoadHtml(allBody);

                foreach(HtmlNode row in allDoc.DocumentNode.SelectNodes("//table[@id='pokedex']/tbody/tr"))
                {
                    IEnumerable<HtmlNode> elements = row.Elements("td");

                    HtmlNode numberNode = elements.ElementAt(0);
                    int number = Int32.Parse(numberNode.LastChild.InnerText.Trim());
                    
                    HtmlNode nameNode = elements.ElementAt(1);
                    HtmlNode majorName = nameNode.Element("a");
                    HtmlNode minorName = nameNode.Element("small");
                    String name;
                    if (minorName == null)
                    {
                        name = majorName.InnerText;
                    }
                    else
                    {
                        name = majorName.InnerText + " - " + minorName.InnerText;
                    }

                    Pokemon p = new Pokemon();
                    p.Number = number;
                    p.Name = name;

                    String url = majorName.GetAttributeValue("href", "");
                    if (url == "")
                    {
                        continue;
                    }

                    String body = client.DownloadString("http://pokemondb.net" + url);
                    HtmlDocument doc = new HtmlDocument();
                    doc.LoadHtml(body);

                    int rowNumber = 0;

                    foreach (HtmlNode vitalsRow in doc.DocumentNode.SelectNodes("//table[@class='vitals-table']/tbody/tr"))
                    {
                        ++rowNumber;
                        HtmlNode td = vitalsRow.Element("td");

                        switch (rowNumber)
                        {
                            case 2:
                                IEnumerable<HtmlNode> anchors = td.Elements("a");
                                List<PokemonType> typeList = new List<PokemonType>();
                                foreach (HtmlNode a in anchors)
                                {
                                    typeList.Add((PokemonType)Enum.Parse(typeof(PokemonType), a.InnerText));
                                }
                                p.Types = typeList.ToArray();
                                break;

                            case 3:
                                p.Species = td.InnerText;
                                break;

                            case 4:
                                p.Height = td.InnerText;
                                break;

                            case 5:
                                p.Weight = td.InnerText;
                                break;

                            case 6:
                                anchors = td.Elements("a");
                                List<string> abilityList = new List<string>();
                                foreach (HtmlNode a in anchors)
                                {
                                    abilityList.Add(a.InnerText);
                                }
                                p.Abilities = abilityList.ToArray();
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
                            Image image = Bitmap.FromStream(stream);
                            MemoryStream memoryStream = new MemoryStream();
                            image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                            p.Picture = memoryStream.GetBuffer();
                        }
                    }

                    db.Store(p);
                    
                    Console.WriteLine(number + "," + name);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Message: {0}", e.Message);
            }
            finally
            {
                db.Close();
            }
        }
    }
}
