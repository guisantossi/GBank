using GBank.Transfer.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GBank.Transfer.Application.Interfaces
{
    public interface ITransferService
    {
        IEnumerable<TransferLog> GetTrasnferLogs();

    }
}
