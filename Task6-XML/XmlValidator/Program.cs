using System;
using System.Text;
using System.Xml;
using System.Xml.Schema;

namespace XmlValidator
{
    class Program
    {
        private static StringBuilder _errorMessage;

        static void Main(string[] args)
        {
            _errorMessage = new StringBuilder();

            var readerSettings = new XmlReaderSettings();
            readerSettings.Schemas.Add("http://library.by/catalog", "books.xsd");
            readerSettings.ValidationType = ValidationType.Schema;
            readerSettings.ValidationFlags |= XmlSchemaValidationFlags.ProcessInlineSchema;
            readerSettings.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;
            readerSettings.ValidationEventHandler += ValidationCallBack;

            using (var reader = XmlReader.Create("books.xml", readerSettings))
            {
                while (reader.Read()) { }
            }

            if (_errorMessage.Length == 0)
            {
                Console.WriteLine("XML is valid");
            }

            Console.WriteLine(_errorMessage);
        }

        private static void ValidationCallBack(object sender, ValidationEventArgs e)
        {
            if (sender is XmlReader element)
            {
                _errorMessage.AppendLine($"{element.Name}[{e.Exception.LineNumber}:{e.Exception.LinePosition}] {e.Message})");
            }
        }
    }
}
