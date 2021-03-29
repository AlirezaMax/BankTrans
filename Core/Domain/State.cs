using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using BankTr.Core.Domain;
using BankTr.Models;

namespace BankTr.Core.Domain
{
    public class State
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(3)]
        public string Code { get; set; }
        
        [StringLength(15)]
        public string Name { get; set; }
        
        public ICollection<Tran> Trans { get; set; }
        //public virtual ICollection<Tran> Trans { get; set; }
    }
}