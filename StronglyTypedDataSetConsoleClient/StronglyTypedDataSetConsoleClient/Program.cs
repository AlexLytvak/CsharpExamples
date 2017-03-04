using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoLotDAL;
using AutoLotDAL.AutoLotDataSetTableAdapters;


namespace StronglyTypedDataSetConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("********Work with Strongly Typed DataSets*****\n");
            AutoLotDataSet.InventoryDataTable table = new AutoLotDataSet.InventoryDataTable();
            InventoryTableAdapter dAdapt = new InventoryTableAdapter();
            AddRecords(table, dAdapt);
            table.Clear();
            dAdapt.Fill(table);
            PrintInventory(table);
            CallStoreProc();
            RemoveRecords(table, dAdapt);
            Console.ReadLine();
        }

        static void PrintInventory(AutoLotDataSet.InventoryDataTable dt)
        {
            for(int curCol=0; curCol <dt.Columns.Count;curCol++)
            {
                Console.Write(dt.Columns[curCol].ColumnName + "\t");
            }
            Console.WriteLine("\n-----------------------------");
            for( int curRow=0;curRow<dt.Rows.Count; curRow++)
            { 
                for (int curCol = 0; curCol < dt.Columns.Count; curCol++)
                {
                    Console.Write(dt.Rows[curRow][curCol].ToString() + "\t");
                }
                Console.WriteLine();
            }
        }

        public static void AddRecords(AutoLotDataSet.InventoryDataTable tb, InventoryTableAdapter dAdapt)
        {
            try
            {
                AutoLotDataSet.InventoryRow newRow = tb.NewInventoryRow();
                //
                newRow.CarID = 991;
                newRow.Color = "Purple";
                newRow.Make = "BMW";
                newRow.PetName = "Saku";
                //
                tb.AddInventoryRow(newRow);
                tb.AddInventoryRow(887, "Yugo", "Green", "Zippy");
                dAdapt.Update(tb);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void RemoveRecords(AutoLotDataSet.InventoryDataTable tb, InventoryTableAdapter dAdapt)
        {
            try {
                AutoLotDataSet.InventoryRow rowToDelete = tb.FindByCarID(991);
                dAdapt.Delete(rowToDelete.CarID, rowToDelete.Make, rowToDelete.Color, rowToDelete.PetName);
                rowToDelete = tb.FindByCarID(887);
                dAdapt.Delete(rowToDelete.CarID, rowToDelete.Make, rowToDelete.Color, rowToDelete.PetName);
                }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void CallStoreProc()
        {
            try
            {
                QueriesTableAdapter q = new QueriesTableAdapter();
                Console.Write("Enter ID of car to look up: ");
                string carID = Console.ReadLine();
                string carName = "";
                q.GetPetName(int.Parse(carID), ref carName);
                Console.WriteLine("carID {0} has the name of {1}", carID, carName);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}
