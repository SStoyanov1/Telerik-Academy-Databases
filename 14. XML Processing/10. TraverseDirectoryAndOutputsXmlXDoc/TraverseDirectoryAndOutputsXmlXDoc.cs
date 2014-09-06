namespace _10.TraverseDirectoryAndOutputsXmlXDoc
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml;
    using System.Xml.Linq;

    public class TraverseDirectoryAndOutputsXmlXDoc
    {
        public static void Main()
        {
            //10. Rewrite the last exercises using XDocument, XElement and XAttribute.

            string startDirectoryPath = @"..\..\..\";

            string xmlResultPath = @"..\..\..\traverseDirectoryXDoc.xml";

            XElement directoriesXml = new XElement("directories");
            TraverseDirectoryRecursively(directoriesXml, startDirectoryPath);

            directoriesXml.Save(xmlResultPath);
        }

        private static void TraverseDirectoryRecursively(XElement element, string directory)
        {
            XElement fileXml = new XElement("files");
            foreach (var file in Directory.GetFiles(directory))
            {
                string fileName = file.ToString();
                int slashIndex = fileName.LastIndexOf(@"\") + 1;
                fileXml.Add(new XElement("name", fileName.Substring(slashIndex)));
            }
            element.Add(fileXml);
            string dirName = directory.ToString();
            int slashDirIndex = dirName.LastIndexOf(@"\") + 1;
            XElement dirElement = new XElement("directory", dirName.Substring(slashDirIndex));
            foreach (var dir in Directory.GetDirectories(directory))
            {
                TraverseDirectoryRecursively(dirElement, dir);
            }
            element.Add(dirElement);
        }
    }
}
