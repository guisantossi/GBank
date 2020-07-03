using GBank.Banking.Application.Interfaces;
using GBank.Banking.Application.Models;
using GBank.Banking.Domain.Commands;
using GBank.Banking.Domain.Interfaces;
using GBank.Banking.Domain.Models;
using GBank.Domain.Core.Bus;
using System;
using System.Collections.Generic;
using System.Text;

namespace GBank.Banking.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IEventBus _eventBus;

        public AccountService(IAccountRepository accountRepository, IEventBus eventBus)
        {
            _accountRepository = accountRepository;
            _eventBus = eventBus;
        }

        public IEnumerable<Account> GetAccounts()
        {
            return _accountRepository.GetAccounts();
        }

        public void Transfer(AccountTransfer accountTransfer)
        {
            var createTransferCommand = new CreateTransferCommand(
                accountTransfer.AccountSource, 
                accountTransfer.AccountTarget, 
                accountTransfer.TranferAmount);

            _eventBus.SendCommand(createTransferCommand);
        }
    }
}
