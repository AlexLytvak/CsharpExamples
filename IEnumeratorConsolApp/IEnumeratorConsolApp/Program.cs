using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IEnumeratorConsolApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Lake myLake = new Lake();
            foreach(Frog f in myLake)
            {
                Console.WriteLine(f.Name);
            }
            Console.Read();
        }
    }
}
