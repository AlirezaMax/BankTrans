using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BankTr.Models;
using BankTr.Core.Domain;

namespace BankTr.ViewModels
{
    public class FindTranViewModel
    {
        public IEnumerable<State> States { get; set; }
        public Tran Tran { get; set; }
        public float MinAmount { get; set; }
        public float MaxAmount { get; set; }
    }
}