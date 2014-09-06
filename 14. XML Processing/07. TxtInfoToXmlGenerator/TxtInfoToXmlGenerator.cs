namespace _07.TxtInfoToXmlGenerator
{
    using System;
    using System.IO;
    using System.Xml;
    using System.Xml.Linq;

    public class TxtInfoToXmlGenerator
    {
        public static void Main()
        {
            //07. In a text file we are given the name, address and phone number of given person
            //(each at a single line). Write a program, which creates new XML document, which
            //contains these data in structured XML format.
            string txtFilePath = @"..\..\..\personalInfo.txt";

            StreamReader reader = new StreamReader(txtFilePath);

            string name = reader.ReadLine();
            string address = reader.ReadLine();
            string phoneNumber = reader.ReadLine();

            XElement booksXml = new XElement("persons",
                new XElement("person",
                new XElement("name", name),
                new XElement("address", address),
                new XElement("phone-number", phoneNumber)
             ));

            booksXml.Save(@"..\..\..\personalInfo.xml");
            Console.WriteLine("XML fle created.");
        }
    }
}
