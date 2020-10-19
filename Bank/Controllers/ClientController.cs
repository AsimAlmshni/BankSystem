using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bank.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ClientController : ControllerBase, ITransactions
    {
        private BankDataAccessLayer bankDataAccessLayer = new BankDataAccessLayer();


        public ClientController() {
        }

        [HttpPost]
        public void Deposite(AccountActionHistory account)
        {
            bankDataAccessLayer.UpdateAccountBalance(account);
        }

        [HttpPost]
        public void Transfer(AccountActionHistory accountTransfer)
        {
            bankDataAccessLayer.DoTransfer(accountTransfer);
        }

        [HttpPost]
        public void Withdraw(AccountActionHistory account)
        {
            bankDataAccessLayer.UpdateAfterWithdraw(account);
        }
    }
}
