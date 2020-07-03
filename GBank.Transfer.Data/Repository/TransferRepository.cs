using GBank.Transfer.Data.Context;
using GBank.Transfer.Domain.Interfaces;
using GBank.Transfer.Domain.Models;
using System.Collections.Generic;

namespace GBank.Transfer.Data.Repository
{
    public class TransferRepository : ITransferRepository
    {

        private TransferDbContext _ctx;

        public TransferRepository(TransferDbContext ctx)
        {
            _ctx = ctx;
        }

        public void Add(TransferLog transferLog)
        {
            _ctx.TransferLogs.Add(transferLog);
            _ctx.SaveChanges();
        }

        public IEnumerable<TransferLog> GetTranferLogs()
        {
            return _ctx.TransferLogs;
        }
    }
}
