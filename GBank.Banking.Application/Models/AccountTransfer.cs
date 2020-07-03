using System;
using System.Collections.Generic;
using System.Text;

namespace GBank.Banking.Application.Models
{
    public class AccountTransfer
    {
        public int AccountSource { get; set; }
        public int AccountTarget { get; set; }

        public decimal TranferAmount { get; set; }
    }
}
