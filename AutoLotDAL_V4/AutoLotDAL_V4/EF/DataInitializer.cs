using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using AutoLotDAL_V4.Models;

namespace AutoLotDAL_V4.EF
{
    public  class DataInitializer:DropCreateDatabaseAlways<AutoLotEntities>
    {
        //protected override void Seed(AutoLotEntities context)
        //{
        //    base.Seed(context);
        //}
        protected override void Seed(AutoLotEntities context)
        {
            var customers = new List<Customer>
            {
                new Customer {FirstName="Dave", LastName="Brenner" },
                new Customer {FirstName="Matt",LastName="Walton" },
                new Customer {FirstName="Steve",LastName="Hagen" },
                new Customer {FirstName="Pat",LastName="Walton" },
                new Customer { FirstName="Bad",LastName="Customer"},
            };
            customers.ForEach(x => context.Customers.Add(x));
            var inventories = new List<Inventory>
            {
                new Inventory { Make="VW", Color="Black",PetName="Zippy"},
                new Inventory { Make="Ford", Color="Rust", PetName="Rusty"},
                new Inventory { Make="Saab", Color="Black", PetName="Mel"},
                new Inventory { Make="Yogo",Color="Yellow", PetName="Clunker"},
                new Inventory { Make="BMW", Color="Black",PetName="Bimmer"},
                new Inventory { Make="BMW",Color="Green", PetName="Hank"},
                new Inventory { Make="BMW", Color="Pink", PetName="Pinky"},
                new Inventory { Make="Pinto", Color="Black", PetName="Pete"},
                new Inventory { Make="Yugo",Color="Brown", PetName="Brownie"}
            };
            inventories.ForEach(x => context.Inventories.Add(x));
            var orders = new List<Order>
            {
                new Order { Inventory=inventories[0],Customer=customers[0]},
                new Order { Inventory=inventories[1],Customer=customers[1]},
                new Order { Inventory=inventories[2],Customer=customers[2]},
                new Order { Inventory=inventories[3], Customer=customers[3]},
            };
            orders.ForEach(x => context.Orders.Add(x));
            //context.CreditRisks.Add(new CreditRisk
            //{
            //    CustId = customers[1].CustId,
            //    FirstName = customers[1].FirstName,
            //    LastName = customers[1].LastName,
            //});
            context.SaveChanges();
        }
    }
}
