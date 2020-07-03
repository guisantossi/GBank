using GBank.Domain.Core.Bus;
using GBank.Transfer.Domain.Events;
using GBank.Transfer.Domain.Interfaces;
using GBank.Transfer.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GBank.Transfer.Domain.EventHandlers
{
    public class TransferEventHandler : IEventHandler<TransferCreatedEvent>
    {
        private readonly ITransferRepository _transferRepository;

        public TransferEventHandler(ITransferRepository transferRepository)
        {
            _transferRepository = transferRepository;
        }
        public Task Handler(TransferCreatedEvent @event)
        {
            _transferRepository.Add(new TransferLog
            {
                FromAccount = @event.From,
                ToAccount = @event.To,
                Amount = @event.Amount
            });

            return Task.CompletedTask;
        }
    }
}
