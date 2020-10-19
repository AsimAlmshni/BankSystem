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
        public bool AuditTransaction(int accountNumberFrom, double amount)
        {

            DateTime localDate = DateTime.Now;
            DateTime utcDate = DateTime.UtcNow;

            var accAmt  = bankDataAccessLayer.GetAccountsBalance(accountNumberFrom);

            if (accAmt >= amount)
                return true;
            else
                return false;
        }

        public void Deposite(int id ,int accountNumber, double amount)
        {
            bankDataAccessLayer.UpdateAccountBalance(id, accountNumber.ToString(), amount);
        }

        [HttpPost]
        public void Transfer(AccountActionHistory accountTransfer)
        {
            bankDataAccessLayer.DoTransfer(accountTransfer);
        }

        public void Withdraw(int id,int accountNumber, double amount)
        {
            if (AuditTransaction(accountNumber, amount) == true)
            {
                bankDataAccessLayer.UpdateAfterWithdraw(id, accountNumber.ToString(), amount);
            }
        }
    }
}
