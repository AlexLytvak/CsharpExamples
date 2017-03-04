using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleEventConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var kesh = new HandlerArg(20);
           // kesh.Alarm();
            kesh.MyEvent += Selector.Message;
            kesh.Alarm();
            Console.Read();
        }
    }
}
