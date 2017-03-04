using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using AutoLotDAL_V4.Models;
using AutoLotDAL_V4.EF;
using static System.Console;

namespace AutoLotDAL_V4.Repos
{
    public class OrderRepo:BaseRepo<Order>
    {
        public OrderRepo()
        {
            Table = Context.Orders;
        }
        public int Delete(int id, byte[]timeStamp)
        {
            Context.Entry(new Order() { OrderId = id,Timestamp=timeStamp }).State = EntityState.Deleted;
            return SaveChanges();
        }
        public Task<int>DeleteAsync(int id, byte[]timeStamp)
        {
            Context.Entry(new Order { OrderId = id,Timestamp=timeStamp }).State = EntityState.Deleted;
            return SaveChangesAsync();
        }

        public static void ShowAllOrdersEagerlyFeched()
        {
            using (var context = new AutoLotEntities())
            {
                WriteLine("********* Pending Orders**********");
                var orders = context.Orders.Include(x => x.Customer).Include(y => y.Inventory).ToList();

                foreach(var itm in orders)
                {
                    WriteLine($"->{itm.Customer.FullName} is waiting on {itm.Inventory.PetName}");
                }
            }
        }

    }
}
