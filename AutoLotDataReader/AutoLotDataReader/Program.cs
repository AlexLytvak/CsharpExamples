using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace AutoLotDataReader
{
    class Program
    {
        static void Main(string[] args)
        {
            using (SqlConnection cn = new SqlConnection())
            {
                cn.ConnectionString = @"Data Source=(local)\SQLEXPRESS;Initial Catalog=AUTOLOT;Integrated Security=True";
                cn.Open();
                         //comand object sql
                string strSQL = "Select * From Inventory";
                SqlCommand myCommand = new SqlCommand(strSQL, cn);
                using (SqlDataReader myDataReader = myCommand.ExecuteReader())
                {
                    while(myDataReader.Read())
                    {
                        Console.WriteLine("->Make:{0}, PetName: {1}, Color: {2}.",
                            myDataReader["Make"].ToString(),
                            myDataReader["PetName"].ToString(),
                            myDataReader["Color"].ToString());
                    }
                }
            }
            Console.ReadLine();
        }
    }
}
