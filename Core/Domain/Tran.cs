using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using BankTr.Core.Domain;
using BankTr.Models;

namespace BankTr.Core.Domain
{
    public class Tran
    {
        [Display(Name = "Transaction Id")]
        public int Id { get; set; }

        public Customer Customer { get; set; }

        [Display(Name = "Customer Id")]
        public int CustomerId { get; set; }

        public float Amount { get; set; }

        public State State { get; set; }

        [Display(Name = "State")]
        public int StateId { get; set; }

        //[StringLength(3)]
        //public string StateCode { get; set; }

        //public virtual Customer Customer { get; set; }

        //public virtual State State { get; set; }

    }
}