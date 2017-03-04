using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegateConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Reporter p = WriteProgressToFile;
            p += WriteProgressToConsole;
            Util.Work(p);
            Console.Read();
        }

        static void WriteProgressToConsole(int perentComlete) => Console.WriteLine(perentComlete);
        static void WriteProgressToFile(int perceComplete) => System.IO.File.WriteAllText("progress.txt", perceComplete.ToString());
    }
}
