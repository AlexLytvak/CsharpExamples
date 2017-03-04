using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoLotDAL_V4.Models;

namespace AutoLotDAL_V4.Repos
{
    public  class CreditRiskRepo:BaseRepo<CreditRisk>
    {
        public CreditRiskRepo()
        {
            Table = Context.CreditRisks;
        }
        public int Delete(int id, byte[]timeStamp)
        {
            Context.Entry(new CreditRisk() { CustId = id, Timestamp=timeStamp }).State = EntityState.Deleted;
            return SaveChanges();
        }
        public Task<int>DeleteAsync(int id, byte[]timeStamp)
        {
            Context.Entry(new CreditRisk() { CustId = id,Timestamp=timeStamp }).State = EntityState.Deleted;
            return SaveChangesAsync();
        }


    }
}
