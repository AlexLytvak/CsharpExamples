using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> myList = new List<string>() { "Kiev", "Harkiv", "Poltava" };
            foreach(var b in myList)
            {
                Console.WriteLine(b);
            }
            Console.WriteLine("//////");
            myList.AddRange(new List<string>() { "Lviv", "Luck", "Bobr" });
            foreach(var b in myList)
            {
                Console.WriteLine(b);
            }
            var str = myList.Find(x => x == "Lviv");
            Console.WriteLine(str);

            Console.Read();
        }
    }
}
