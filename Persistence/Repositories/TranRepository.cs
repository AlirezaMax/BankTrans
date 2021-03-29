using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using BankTr.Models;
using BankTr.Core.Domain;

namespace BankTr.Models
{
    public class TranRepository : Repository<Tran>, ITranRepository
    {
        public TranRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public IEnumerable<Tran> GetAllTrans()
        {
            return ApplicationDbContext.Trans.Include(c => c.State).Include(c => c.Customer).
               OrderBy(c => c.StateId).ToList();
        }

        public IEnumerable<Tran> GetTopTrans(int count)
        {
             var topTrans = ApplicationDbContext.Trans.Include(c => c.State).Include(c => c.Customer).
                OrderByDescending(c => c.Amount).Take(count).ToList();

            return topTrans;
        }

        public IEnumerable<Tran> GetTransPerState(int stCode)
        {
            return ApplicationDbContext.Trans.Include(c => c.State).Include(c => c.Customer)
                .Where(e => e.StateId == stCode).OrderByDescending(c => c.Amount).ToList();
        }

        public IEnumerable<Tran> GetTopTransPerState(int count1, int count2, int stCode)
        {
            try
            {
                return ApplicationDbContext.Trans.Include(c => c.State).Include(c => c.Customer)
                    .Where(e => e.StateId == stCode).OrderByDescending(c => c.Amount)
                    .Take(count2).Skip(count1).ToList();
            }
            catch 
            { return ApplicationDbContext.Trans.Include(c => c.State).Include(c => c.Customer)
                    .Where(e => e.StateId == 2).Where(c => c.Amount == 0).ToList(); 
            }
        }        

        public IEnumerable<Tran> GetMaxListInAllStates()
        {
            var context = new ApplicationDbContext();

            //List<Tran> transMaxAllStates = new List<Tran>();

            var transMaxAllStates = ApplicationDbContext.Trans.Include(c => c.State)
                    .Where(e => e.Amount == 0).ToList();

            foreach (var state in context.States)
            {   
                try
                {
                    var tranMax = ApplicationDbContext.Trans.Include(c => c.State)
                    .Where(e => e.StateId == state.Id).OrderByDescending(c => c.Amount).Take(1).ToList();

                    transMaxAllStates.Add(tranMax[0]);
                }
                catch
                {/*transMaxAllStates.Add(new Tran(){ Amount = 0, StateId = state.Id });*/}                
            }
            return transMaxAllStates;
        }

        public IEnumerable<Tran> GetSumListInAllStates()
        {
            var context = new ApplicationDbContext();

            var transSumAllStates = ApplicationDbContext.Trans.Include(c => c.State)
                    .Where(e => e.Amount == 0).ToList();

            foreach (var state in context.States)
            {
                try
                {
                    /*float tranSum = ApplicationDbContext.Trans.Include(c => c.State)
                    .Where(e => e.StateId == state.Id).Sum(c => c.Amount);

                    transSumAllStates.Add(new Tran()
                    { Amount = tranSum, StateId = state.Id });*/

                    var tranSum = ApplicationDbContext.Trans.Include(c => c.State)
                    .Where(e => e.StateId == state.Id).OrderByDescending(c => c.Amount).Take(1).ToList();

                    tranSum[0].Amount = ApplicationDbContext.Trans.Include(c => c.State)
                    .Where(e => e.StateId == state.Id).Sum(c => c.Amount);

                    transSumAllStates.Add(tranSum[0]);
                }
                catch
                {   /*transSumAllStates.Add(new Tran(){ Amount = 0, StateId = state.Id });*/}
            }
            return transSumAllStates;
        }

        public IEnumerable<Tran> GetTopTransAllState(int count1, int count2)
        {
            var context = new ApplicationDbContext();

            //List<Tran> topTransAllState = new List<Tran>();
            var topTransAllState = ApplicationDbContext.Trans.Include(c => c.State)
                    .Where(e => e.Amount == 0).ToList();

            foreach (var state in context.States)
            {
                var topTransPerState = ApplicationDbContext.Trans.Include(c => c.State)
                .Where(e => e.StateId == state.Id).OrderByDescending(c => c.Amount)
                .Take(count2).Skip(count1).ToList();
                //.Include(c => c.State).Include(c => c.Customer)
                int i = 0;
                foreach (var tr in topTransPerState)
                {
                    topTransAllState.Add(topTransPerState[i]);
                    i = 1 + i;
                }
            }
            return topTransAllState;
        }

        public IEnumerable<Tran> FindTrans(int stId, float minAmount, float maxAmount)
        {
            if (maxAmount == 0) maxAmount = 1000000;
            if (stId > 0)
            return ApplicationDbContext.Trans.Include(c => c.State).Include(c => c.Customer)
                .Where(e => e.StateId == stId && e.Amount > minAmount && e.Amount < maxAmount)
                .OrderByDescending(c => c.Amount).ToList();
            else 
                return ApplicationDbContext.Trans.Include(c => c.State).Include(c => c.Customer)
                .Where(e => e.Amount > minAmount && e.Amount < maxAmount)
                .OrderByDescending(c => c.Amount).ToList();
        }

        public Tran FindTranById(int tranId)
        {
            return ApplicationDbContext.Trans
                .SingleOrDefault(e => e.Id == tranId);
        }

        public float GetMaxInState(int stCode)
        {
            try { return ApplicationDbContext.Trans.Include(c => c.State).Include(c => c.Customer)
                    .Where(e => e.StateId == stCode).Max(c => c.Amount); }
            catch { return 0; }
            //Where(e => e.StateId == stCode).  c => c.Amount
        }

        public float GetSumInState(int stCode)
        {
            try { return ApplicationDbContext.Trans.Include(c => c.State).Include(c => c.Customer)
                    .Where(e => e.StateId == stCode).Sum(c => c.Amount); }
            catch { return 0; }
        }

        public int GetTransInStateCount(int stCode)
        {
            try { return ApplicationDbContext.Trans.Include(c => c.State).Include(c => c.Customer)
                    .Where(e => e.StateId == stCode).Count(); }
            catch { return 0; }
        }

        public ApplicationDbContext ApplicationDbContext
        {
            get { return Context as ApplicationDbContext; }
        }
    }
}