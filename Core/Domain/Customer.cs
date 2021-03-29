using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using BankTr.Core.Domain;
using BankTr.Models;

namespace BankTr.Core.Domain
{
    public class Customer
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public virtual ICollection<Tran> Trans { get; set; }

    }
}