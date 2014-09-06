namespace _02_06.XMLExtractor
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml;
    using System.Xml.Linq;

    public class XMLOperations
    {
        static void Main()
        {
            string xmlPath = @"..\..\..\catalog.xml";

            XmlDocument catalogDoc = new XmlDocument();
            catalogDoc.Load(xmlPath);

            //02. Write program that extracts all different artists which are found in the catalog.xml.
            //For each author you should print the number of albums in the catalogue. Use the DOM parser and a hash-table.
            PrintAuthorsAndNumberOfAlbums(catalogDoc);

            //03. Implement the previous using XPath.
            PrintAuhorsAndNumberOfAlbumsXPath(catalogDoc);

            //04. Using the DOM parser write a program to delete from catalog.xml all albums having price > 20.
            RemoveAlbumsWithPriceHigherThan(20, catalogDoc);

            //05. Write a program, which using XmlReader extracts all song titles from catalog.xml.
            PrintsAllSongsTitles(xmlPath);

            //06. Rewrite the same using XDocument and LINQ query.
            PrintsAllSongTitlesLINQ(xmlPath);
        }

        private static void PrintsAllSongTitlesLINQ(string xmlPath)
        {
            XDocument catalogDocX = XDocument.Load(xmlPath);
            var songTitles = catalogDocX.Descendants("title").Select(t => t.Value);

            foreach (var songtitle in songTitles)
            {
                Console.WriteLine(songtitle);
            }
        }

        private static void PrintsAllSongsTitles(string xmlPath)
        {
            using (XmlReader reader = XmlReader.Create(xmlPath))
            {
                while (reader.Read())
                {
                    if ((reader.NodeType == XmlNodeType.Element) &&
                        (reader.Name == "title"))
                    {
                        Console.WriteLine(reader.ReadElementString());
                    }
                }
            }
            Console.WriteLine();
        }

        private static void RemoveAlbumsWithPriceHigherThan(decimal priceMax, XmlDocument catalogDoc)
        {
            var nodesToRemove = new List<XmlNode>();
            foreach (XmlNode node in catalogDoc.DocumentElement)
            {
                decimal price = decimal.Parse(node["price"].InnerText);
                if (price > priceMax)
                {
                    nodesToRemove.Add(node);
                }
            }

            foreach (var node in nodesToRemove)
            {
                catalogDoc.DocumentElement.RemoveChild(node);
            }

            Console.WriteLine("Modified XML document.");
            Console.WriteLine();
            catalogDoc.Save(@"..\..\..\catalogDeletedAlbums.xml");
        }

        private static void PrintAuhorsAndNumberOfAlbumsXPath(XmlDocument catalogDoc)
        {
            string xPathQuery = "catalog/album/artist";

            XmlNodeList artistNames = catalogDoc.SelectNodes(xPathQuery);
            var result = artistNames.OfType<XmlElement>().GroupBy(e => e.InnerText).ToDictionary(gr => gr.Key, gr => gr.Count());

            PrintDictionaryArtistsAndNumberOfAlbums(result);
            Console.WriteLine();
        }

        private static void PrintAuthorsAndNumberOfAlbums(XmlDocument catalogDoc)
        {
            XmlNode rootNode = catalogDoc.DocumentElement;
            var artistsNumberAlbums = new Dictionary<string, int>();

            foreach (XmlNode node in rootNode.ChildNodes)
            {
                string artist = node["artist"].InnerText;
                if (!artistsNumberAlbums.ContainsKey(artist))
                {
                    artistsNumberAlbums[artist] = 0;
                }
                artistsNumberAlbums[artist]++;
            }

            PrintDictionaryArtistsAndNumberOfAlbums(artistsNumberAlbums);
            Console.WriteLine();
        }

        private static void PrintDictionaryArtistsAndNumberOfAlbums(IDictionary<string, int> artistsNumberAlbums)
        {
            foreach (var artist in artistsNumberAlbums)
            {
                Console.WriteLine("{0} -> {1} {2}", artist.Key, artist.Value, artist.Value == 1 ? "album" : "albums");
            }
        }
    }
}
