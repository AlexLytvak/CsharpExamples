using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Threading;

namespace DelegateConsoleApp
{
    public delegate void Reporter(int n);
    public   class Util
    {
        public static void Work(Reporter r)
        {
            for(int i=0;i<10;i++)
            {
                r(i * 10);
                System.Threading.Thread.Sleep(100);
            }
        }
    }
}
