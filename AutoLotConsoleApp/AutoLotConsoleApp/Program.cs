using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using System.Data.Entity.Infrastructure;
using AutoLotConsoleApp.EF;

namespace AutoLotConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("****Work with ADO.NET EF*****\n");
            //int carId = AddNewrecord();
            // WriteLine(carId);
            // PrintAllInventory();
            // FunWithLinqQueries();
            // RemoveRecord(8);

            UpdateRecord(4);

            ReadLine();
        }

        private static int AddNewrecord()
        {
            using (var context = new AutoLotEntities())
            {
                try
                {
                    var car = new Car() { Make = "Yugo", Color = "Brown", CarNickName = "Brownie" };
                    context.Cars.Add(car);
                    context.SaveChanges();
                    return car.CarId;
                }
                catch (Exception ex)
                {
                    WriteLine(ex.InnerException.Message);
                    return 0;
                }
            }
        }
        
        private static void PrintAllInventory()
        {
            using (var context = new AutoLotEntities())
            {
                foreach(Car c in context.Cars)
                {
                    WriteLine(c);
                }
                WriteLine("SQL");
                foreach(Car c in context.Cars.SqlQuery("Select CarId, Make, Color, PetName as CarNickName from Inventory where Make=@p0","BMW"))
                {
                    WriteLine(c);
                }
                WriteLine("LINQ");
                foreach(Car c in context.Cars.Where(c=>c.Make=="BMW"))
                {
                    WriteLine(c);
                }
            }
        }

        private static void FunWithLinqQueries()
        {
            using (var context = new AutoLotEntities())
            {
                var colorsMakes = from item in context.Cars select new { item.Color, item.Make };
                foreach(var item in colorsMakes)
                {
                    WriteLine(item);
                }

                WriteLine("Color==Black");
                var blackCars = from item in context.Cars where item.Color == "Black" select item;
                foreach(var item in blackCars)
                {
                    WriteLine(item);
                }
                WriteLine("LINQ to Object");
                // var AllData = (from item in context.Cars select item).ToArray();
                var AllData = context.Cars.ToArray();
                //
                var colorsMakes2 = from item in AllData select new { item.Color, item.Make };
                foreach(var item in colorsMakes2)
                {
                    WriteLine(item);
                }
                WriteLine("Color is Black");
                var blackCars2 = from item in AllData where item.Color == "Black" select item;
                foreach(var item in blackCars2)
                {
                    WriteLine(item);
                }

            }
        }

        private static void RemoveRecord(int carId)
        {
            using (var context = new AutoLotEntities())
            {
                Car carToDelete = context.Cars.Find(carId);
                if(carToDelete!=null)
                {
                    context.Cars.Remove(carToDelete);
                    context.SaveChanges();
                }
            }
        }

        private static void RemoveRecordUsingEntityState(int carId)
        {
            using (var context = new AutoLotEntities())
            {
                Car carToDelete = new Car() { CarId = carId };
                context.Entry(carToDelete).State = System.Data.Entity.EntityState.Deleted;
                try
                {
                    context.SaveChanges();
                }
                catch(DbUpdateConcurrencyException ex)
                {
                    WriteLine(ex);
                }
            }
        }

        private static void UpdateRecord(int carId)
        {
            using (var context = new AutoLotEntities())
            {
                Car carToUpdate = context.Cars.Find(carId);
                if(carToUpdate!=null)
                {
                    WriteLine(context.Entry(carToUpdate).State);
                    carToUpdate.Color = "Blue";
                    WriteLine(context.Entry(carToUpdate).State);
                    context.SaveChanges();
                }
            }
        }

    }
}
