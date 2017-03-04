using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoLotConnectedLayer;

namespace AdoNetTransaction
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***Transaction Example****");
            bool throwEx = true;
            string userAnswer = string.Empty;
            Console.WriteLine("Do you want to throw en exception ( Y or N): ");
            userAnswer = Console.ReadLine();
            if(userAnswer.ToLower()=="n")
            {
                throwEx = false;
            }
            InventoryDAL dal = new InventoryDAL();
            dal.OpenConnection(@"Data Source=DESKTOP-I3V8QK7\SQLEXPRESS;Initial Catalog=AUTOLOT;Integrated Security=True");
            dal.ProcessCreditRisk(throwEx, 333);
            Console.WriteLine("Check CreditRisk table for results");
            Console.ReadLine();
        }
    }
}
