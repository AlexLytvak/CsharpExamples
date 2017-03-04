namespace AutoLotDAL_V4.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using AutoLotDAL_V4.Models;
    using System.Collections.Generic;

    internal sealed class Configuration : DbMigrationsConfiguration<AutoLotDAL_V4.EF.AutoLotEntities>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "AutoLotDAL_V4.EF.AutoLotEntities";
        }

        protected override void Seed(AutoLotDAL_V4.EF.AutoLotEntities context)
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
            orders.ForEach(x => context.Orders.AddOrUpdate(o => new { o.CarId, o.CustId }, x));
            context.CreditRisks.AddOrUpdate(c => new { c.FirstName, c.LastName },
               new CreditRisk
               {
                   CustId = customers[4].CustId,
                   FirstName = customers[4].FirstName,
                   LastName = customers[4].LastName,
               }
                );
        }
    }
}
