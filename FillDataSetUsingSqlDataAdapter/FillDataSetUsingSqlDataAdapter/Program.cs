using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;

namespace FillDataSetUsingSqlDataAdapter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("**** Work with Data Adapters*****");
            string cnStr = @"Data Source=(local)\SQLEXPRESS;Initial Catalog=AUTOLOT;Integrated Security=True";
            DataSet ds = new DataSet("AUTOLOT");
            //
            
            //adppter
            SqlDataAdapter dAdapt = new SqlDataAdapter("Select * From Inventory", cnStr);
            //
            DataTableMapping custMap = dAdapt.TableMappings.Add("Inventory", "Current Inventory");
            custMap.ColumnMappings.Add("CarID", "Car ID");
            custMap.ColumnMappings.Add("CarID", "Car ID");
            custMap.ColumnMappings.Add("PetName", "Name of Car");
           
            dAdapt.Fill(ds, "Inventory");
            PrintDataSet(ds);
            Console.ReadLine();
        }

        static void PrintDataSet(DataSet ds)
        {
            Console.WriteLine("DataSet is named: {0}", ds.DataSetName);
            foreach(System.Collections.DictionaryEntry de in ds.ExtendedProperties)
            {
                Console.WriteLine("Key={0}, Value={1}", de.Key, de.Value);
            }
            Console.WriteLine();
            foreach(DataTable dt in ds.Tables)
            {
                Console.WriteLine("=> {0} Table:", dt.TableName);
                //columns
                for(int curCol=0; curCol<dt.Columns.Count; curCol++)
                {
                    Console.Write(dt.Columns[curCol].ColumnName.Trim() + "\t");
                }
                Console.WriteLine("\n------------------------------------");
                DataTableReader dtReader = dt.CreateDataReader();
                while(dtReader.Read())
                {
                    for(int i=0;i<dtReader.FieldCount;i++)
                    {
                        Console.Write("{0}\t", dtReader.GetValue(i).ToString().Trim());
                    }
                    Console.WriteLine();
                }
               // dtReader.Close();
            }
        }
    }
}
