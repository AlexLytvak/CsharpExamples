using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ParseAndLoadStringAsXml
{
    class Program
    {
        static void Main(string[] args)
        {
            string myElement = @"<Car ID='3'> <Color>Yellow</Color> <Make>Yugo</Make></Car>";
            ParseAndLoadExistringXml(myElement);

            Console.Read();
        }
        static void ParseAndLoadExistringXml(string str)
        {
            // string myElement = @"<Car ID='3'> <Color>Yellow</Color> <Make>Yugo</Make></Car>";
            XElement newElement = XElement.Parse(str);
            Console.WriteLine(newElement);
            Console.WriteLine();
            XDocument myDoc = XDocument.Load("SimpleInventory.xml");
            Console.WriteLine(myDoc);

        }
    }
}
