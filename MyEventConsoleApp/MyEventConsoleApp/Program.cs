using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEventConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var newshop = new Shop("20%");
            newshop.SeampleEvent += ShopEvent;
            newshop.ActivEvent();
            Console.ReadLine();
        }

        public static void ShopEvent()
        {
            Console.WriteLine("Seales!!!!!");
        }
        
    }
}
