using GBank.Banking.Application.Models;
using GBank.Banking.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GBank.Banking.Application.Interfaces
{
    public interface IAccountService
    {
        IEnumerable<Account> GetAccounts();
        void Transfer(AccountTransfer accountTransfer);

    }
}
