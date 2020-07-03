using GBank.Banking.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GBank.Banking.Domain.Interfaces
{
    public interface IAccountRepository
    {
        IEnumerable<Account> GetAccounts();
    }
}
