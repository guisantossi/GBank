using GBank.Transfer.Domain.Models;
using System.Collections.Generic;

namespace GBank.Transfer.Domain.Interfaces
{
    public interface ITransferRepository
    {
        IEnumerable<TransferLog> GetTranferLogs();
        void Add(TransferLog transferLog);
    }
}
