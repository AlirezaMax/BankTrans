using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankTr.Core.Domain;

namespace BankTr.Models
{
    public interface ITranRepository : IRepository<Tran>
    {
        IEnumerable<Tran> GetAllTrans();

        IEnumerable<Tran> GetTopTrans(int count);

        IEnumerable<Tran> GetTransPerState(int stCode);

        IEnumerable<Tran> GetTopTransPerState(int count1, int count2, int stCode);
        // Take count2 and Skip count1,for example second top 3 would be count1= 3 and count2= 6 

        IEnumerable<Tran> GetTopTransAllState(int count1, int count2);
        
        IEnumerable<Tran> GetMaxListInAllStates();

        IEnumerable<Tran> GetSumListInAllStates();

        IEnumerable<Tran> FindTrans(int stId, float minAmount, float maxAmount);

        Tran FindTranById(int tranId);

        float GetMaxInState(int stCode);

        float GetSumInState(int stCode);

        int GetTransInStateCount(int stCode);

    }
}
