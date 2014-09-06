namespace _08.AlbumXmlCreator
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Xml;
    using System.Xml.Linq;

    public class AlbumXmlCreator
    {
        public static void Main()
        {
            //08. Write a program, which (using XmlReader and XmlWriter) reads the file catalog.xml
            //and creates the file album.xml, in which stores in appropriate way the names of all
            //albums and their authors.
            string xmlPath = @"..\..\..\catalog.xml";

            var albumArtistDict = new Dictionary<string, string>();

            using (XmlReader reader = XmlReader.Create(xmlPath))
            {
                while (reader.Read())
                {
                    if ((reader.NodeType == XmlNodeType.Element) &&
                        (reader.Name == "name"))
                    {
                        string albumName = reader.ReadElementContentAsString();
                        if (reader.ReadToNextSibling("artist"))
                        {
                            string artistName = reader.ReadElementContentAsString();
                            albumArtistDict.Add(albumName, artistName);
                        }
                    }
                }
            }

            string xmlResultPath = @"..\..\..\album.xml";
            Encoding encoding = Encoding.GetEncoding("windows-1251");
            using (XmlTextWriter writer = new XmlTextWriter(xmlResultPath, encoding))
            {
                writer.Formatting = Formatting.Indented;
                writer.IndentChar = '\t';
                writer.Indentation = 1;

                writer.WriteStartDocument();
                writer.WriteStartElement("catalog");
                writer.WriteAttributeString("name", "Catalog of Albums");
                foreach (var album in albumArtistDict)
	            {
		            writer.WriteStartElement("album");
                    writer.WriteElementString("name", album.Key);
                    writer.WriteElementString("artist", album.Value);
                    writer.WriteEndElement();
	            }
                writer.WriteEndDocument();
            }
        }
    }
}
