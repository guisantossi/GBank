using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GBank.Banking.Application.Interfaces;
using GBank.Banking.Application.Models;
using GBank.Banking.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace GBank.Banking.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankingController : ControllerBase
    {
        // GET api/banking

        private readonly IAccountService _accountService;

        public BankingController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Account>> Get()
        {
            return Ok(_accountService.GetAccounts());
        }

        [HttpPost]
        public ActionResult Post([FromBody] AccountTransfer accountTransfer)
        {
            _accountService.Transfer(accountTransfer);
            return Ok(accountTransfer);
        }

    }
}
