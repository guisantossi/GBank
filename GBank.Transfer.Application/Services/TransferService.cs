using GBank.Domain.Core.Bus;
using GBank.Transfer.Application.Interfaces;
using GBank.Transfer.Domain.Interfaces;
using GBank.Transfer.Domain.Models;
using System.Collections.Generic;

namespace GBank.Transfer.Application.Services
{
    public class TransferService : ITransferService
    {
        private readonly ITransferRepository _transferRepository;
        private readonly IEventBus _eventBus;

        public TransferService(ITransferRepository transferRepository, IEventBus eventBus)
        {
            _transferRepository = transferRepository;
            _eventBus = eventBus;
        }
        public IEnumerable<TransferLog> GetTrasnferLogs()
        {
            return _transferRepository.GetTranferLogs();

        }
    }
}
