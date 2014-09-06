namespace _13.XSLTransformation
{
    using System.Xml.Xsl;

    public class XSLTransformation
    {

        public static void Main()
        {
            XslCompiledTransform xslt = new XslCompiledTransform();
            xslt.Load("../../catalog.xsl");
            xslt.Transform("../../catalog.xml", "../../catalog.html");
        }
    }
}