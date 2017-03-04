using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeleagateApp
{
    public delegate void Progress(int p);
    class Program
    {
        static void Main(string[] args)
        {
            X x = new X();
            Progress p = x.InstanceProgress;
            p(10);
           // Console.Read();
            Console.WriteLine(p.Target == x);
            Console.WriteLine(p.Method);
            Console.Read();
        }
    }
    public class X
    {
        public void InstanceProgress(int n) => Console.WriteLine(n);
        
    }
}
