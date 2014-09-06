namespace _11.ExtractAlbumsPrices
{
    using System;
    using System.Xml;

    public class ExtractAlbumsPrices
    {
        public static void Main()
        {
            //11. Write a program, which extract from the file catalog.xml the prices for all albums,
            //published 5 years ago or earlier. Use XPath query.

            string filePath = @"..\..\..\catalog.xml";

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);
            string xPathQuery = string.Format("/catalog/album[year>{0}]/price", DateTime.Now.Year - 5);

            var pricesOfFiveYearsOrLaterAlbums = xmlDoc.SelectNodes(xPathQuery);
            foreach (XmlNode price in pricesOfFiveYearsOrLaterAlbums)
            {
                Console.WriteLine("${0}", price.InnerText); 
            }
        }
    }
}
