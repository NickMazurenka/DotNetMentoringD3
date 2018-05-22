using System;
using System.Xml;
using System.Xml.Schema;

namespace XmlValidator
{
    class Program
    {
        static void Main(string[] args)
        {
            var readerSettings = new XmlReaderSettings();
            readerSettings.Schemas.Add("http://library.by/catalog", "books.xsd");
            readerSettings.ValidationType = ValidationType.Schema;
            readerSettings.ValidationEventHandler += new ValidationEventHandler(booksSettingsValidationEventHandler);

            var books = XmlReader.Create("books.xml", readerSettings);

            while (books.Read()) { }
        }

        static void booksSettingsValidationEventHandler(object sender, ValidationEventArgs e)
        {
            if (e.Severity == XmlSeverityType.Warning)
            {
                Console.Write("WARNING: ");
                Console.WriteLine(e.Message);
            }
            else if (e.Severity == XmlSeverityType.Error)
            {
                Console.Write("ERROR: ");
                Console.WriteLine(e.Message);
            }
        }
    }
}
