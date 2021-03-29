using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BankTr.Models;
using BankTr.Core.Domain;

namespace BankTr.ViewModels
{
    public class NewTranViewModel
    {
        public IEnumerable<State> States { get; set; }
        public Tran Tran { get; set; }
    }
}