namespace _09.TraverseDirectoryAndOutputsXml
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml;
    using System.Xml.Linq;

    public class TraverseDirectoryAndOutputsXml
    {
        public static void Main()
        {
            //09. Write a program to traverse given directory and write to a XML file its contents
            //together with all subdirectories and files. Use tags <file> and <dir> with appropriate
            //attributes. For the generation of the XML document use the class XmlWriter.

            string startDirectoryPath = @"..\..\..\";

            string xmlResultPath = @"..\..\..\traverseDirectory.xml";
            Encoding encoding = Encoding.GetEncoding("windows-1251");
            XmlTextWriter writer = new XmlTextWriter(xmlResultPath, encoding);
            using (writer)
            {
                writer.Formatting = Formatting.Indented;
                writer.IndentChar = '\t';
                writer.Indentation = 1;

                writer.WriteStartDocument();
                writer.WriteStartElement("directories");
                TraverseDirectoryRecursively(ref writer, startDirectoryPath);
                writer.WriteEndDocument();
            }
        }

        private static void TraverseDirectoryRecursively(ref XmlTextWriter writer, string directory)
        {
            writer.WriteStartElement("directory");
            string dirName = directory.ToString();
            int slashDirIndex = dirName.LastIndexOf(@"\") + 1;
            writer.WriteElementString("name", dirName.Substring(slashDirIndex));
            writer.WriteStartElement("files");
            foreach (var file in Directory.GetFiles(directory))
            {
                writer.WriteStartElement("file");
                string fileName = file.ToString();
                int slashIndex = fileName.LastIndexOf(@"\") + 1;
                writer.WriteElementString("name", fileName.Substring(slashIndex));
                writer.WriteEndElement();
            }
            writer.WriteEndElement();

            foreach (var dir in Directory.GetDirectories(directory))
            {
                TraverseDirectoryRecursively(ref writer, dir);
            }
            writer.WriteEndElement();
        }
    }
}
