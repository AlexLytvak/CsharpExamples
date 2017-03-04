using System;
using System.Configuration;
using AutoLotConnectedLayer;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace AutoLotCUIClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("****The AutoLot Console UI*****");
            string cnStr = ConfigurationManager.ConnectionStrings["AutoLotSqlProvider"].ConnectionString;
            bool userDone = false;
            string userCommand = "";
            InventoryDAL invDAL = new InventoryDAL();
            invDAL.OpenConnection(cnStr);

            try

            {
                ShowInstructions();
                do
                {
                    Console.WriteLine("\nEnter yuor command: ");
                    userCommand = Console.ReadLine();
                    Console.WriteLine();
                    switch (userCommand.ToUpper())
                    {
                        case "I":
                            InsertNewCar(invDAL);
                            break;
                        case "U":
                            UpdateCarPetName(invDAL);
                            break;
                        case "D":
                            DeleteCar(invDAL);
                            break;
                        case "L":
                            ListInventoryViaList(invDAL);
                            break;
                        case "S":
                            ShowInstructions();
                            break;
                        case "P":
                            LookUpPetName(invDAL);
                            break;
                        case "Q":
                            userDone = true;
                            break;
                        default:
                            Console.WriteLine("Bad data ! Try again ");
                            break;
                    }
                }
                while (!userDone);
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                invDAL.CloseConnection();
            }
        }

        private static void ShowInstructions()
        {
            Console.WriteLine("I : Inserts a new car.");
            Console.WriteLine("U: Updates an existing car.");
            Console.WriteLine("D: Deletes an existing car.");
            Console.WriteLine("L: Lists current inventory.");
            Console.WriteLine("S: Shows these instructions.");
            Console.WriteLine("P: Looks up pet name.");
            Console.WriteLine("Q: Quits program.");
        }

        private static void ListInventoryViaList(InventoryDAL invDAL)
        {
            List<NewCar> record = invDAL.GetAllInventoryAsList();
            foreach(NewCar c in record)
            {
                Console.WriteLine("CarID: {0}, Make: {1}, Color :{2}, PetName: {3} ", c.CarID, c.Make, c.Color, c.PetName);
            }
        }

        private static void DeleteCar(InventoryDAL invDAL)
        {
            Console.WriteLine("Enter ID of Car delete: ");
            int id = int.Parse(Console.ReadLine());
            try
            {
                invDAL.DeleteCar(id);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
                
        }

        private static void InsertNewCar(InventoryDAL invDAL)
        {
            int newCarID;
            string newCarColor, newCarMake, newCarPetName;
            Console.Write("Enter Car ID: ");
            newCarID = int.Parse(Console.ReadLine());
            Console.Write(" Enter Car Color: ");
            newCarColor = Console.ReadLine();
            Console.Write("Enter Car Make: ");
            newCarMake = Console.ReadLine();
            Console.Write("Enter Pet Name :");
            newCarPetName = Console.ReadLine();
            invDAL.InsertAuto(newCarID, newCarColor, newCarMake, newCarPetName);
        }

        private static void UpdateCarPetName(InventoryDAL invDAL)
        {
            int carID;
            string newCarPetName;
            Console.WriteLine("Enter Car ID");
            carID = int.Parse(Console.ReadLine());
            Console.Write("Enter new Pet Name ");
            newCarPetName = Console.ReadLine();
            invDAL.UpdateCarPetName(carID, newCarPetName);
        }

        private static void LookUpPetName(InventoryDAL invDAL)
        {
            Console.WriteLine("Enter ID of Car to look up");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine("Petname of {0} is {1}", id, invDAL.LookUpPetName(id).TrimEnd());
        }


    }
}
