using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankTr.Core.Domain;

namespace BankTr.Models
{
    public interface IUnitOfWork : IDisposable
    {
        ITranRepository Trans { get; }
        //ICustomerRepository Customers { get; }
        int Complete();
    }
}
