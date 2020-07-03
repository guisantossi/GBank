using GBank.Transfer.Application.Interfaces;
using GBank.Transfer.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace GBank.Transfer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransferController : ControllerBase
    {
        // GET api/banking

        private readonly ITransferService _transferService;

        public TransferController(ITransferService transferService)
        {
            _transferService = transferService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TransferLog>> Get()
        {
            return Ok(_transferService.GetTrasnferLogs());
        }
    }
}
