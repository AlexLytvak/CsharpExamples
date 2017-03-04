
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
//using System.Data.Entity.Infrastructure;
using AutoLotDAL_V4.Models;


namespace AutoLotDAL_V4.Repos
{
    public class InventoryRepo:BaseRepo<Inventory>
    {
        public InventoryRepo()
        {
            Table = Context.Inventories;
        }
        public int Delete(int id,byte[] timeStamp)
        {
            Context.Entry(new Inventory() { CarId = id ,Timestamp=timeStamp}).State = EntityState.Deleted;
            return SaveChanges();
        }
        public Task<int>DeleteAsync(int id,byte[] timeStamp)
        {
            Context.Entry(new Inventory() { CarId = id,Timestamp=timeStamp }).State = EntityState.Deleted;
            return SaveChangesAsync();
        }

    }
}
