using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BankTr.Core.Domain;

namespace BankTr.Models
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Trans = new TranRepository(_context);
            //Customers = new CustomerRepository(_context);
        }

        public ITranRepository Trans { get; private set; }
        //public ICustomerRepository Customers { get; private set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}