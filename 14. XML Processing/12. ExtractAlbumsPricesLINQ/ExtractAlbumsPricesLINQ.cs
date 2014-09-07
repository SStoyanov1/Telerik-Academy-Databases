namespace _12.ExtractAlbumsPricesLINQ
{
    using System;
    using System.Linq;
    using System.Xml;
    using System.Xml.Linq;

    public class ExtractAlbumsPricesLINQ
    {
        public static void Main()
        {
            //12. Rewrite the previous using LINQ query.

            string filePath = @"..\..\..\catalog.xml";

            var document = XDocument.Load(filePath);
            var prices = document.Descendants("album")
                .Where(album => int.Parse(album.Descendants("year").FirstOrDefault().Value) <= 2009)
                .Select(album => album.Descendants("price").FirstOrDefault().Value);
            //
            //XDocument catalogDocX = XDocument.Load(filePath);
            //var priceOfFiveYearsOrLaterAlbums = catalogDocX.Descendants("album").Where(a => int.Parse(a.Element("year").Value) >= DateTime.Now.Year - 5).Elements("price");
            //
            //foreach (XElement price in priceOfFiveYearsOrLaterAlbums)
            //{
            //    Console.WriteLine("${0}", price.Value);
            //}
            foreach (string item in prices)
            {
                Console.WriteLine(item);
            }
        }
    }
}
