using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Stock stock = new Stock("THPW");
            Console.WriteLine("one");
            stock.Price = 27.10M;
            Console.WriteLine("two");
            stock.PriceChanged += stock_PriceChanged;
            Console.WriteLine("Three");
            stock.Price = 31.59M;
            Console.WriteLine("Four");
            Console.ReadLine();
        }
        static void stock_PriceChanged(object sender, PriceChangedEventArgs e)
        {
            if ((e.NewPrice - e.LastPrice) / e.LastPrice > 0.1M)
                Console.WriteLine("Alert, 10% stock price increase!");
        }
    }
}
