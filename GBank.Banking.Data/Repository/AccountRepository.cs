using GBank.Banking.Data.Context;
using GBank.Banking.Domain.Interfaces;
using GBank.Banking.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GBank.Banking.Data.Repository
{
    public class AccountRepository : IAccountRepository
    {

        private BankingDbContext _ctx;

        public AccountRepository(BankingDbContext ctx)
        {
            _ctx = ctx;
        }

        public IEnumerable<Account> GetAccounts()
        {
            return _ctx.Accounts;
        }
    }
}
