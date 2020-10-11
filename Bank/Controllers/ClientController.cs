using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bank.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Controllers
{
    [Route("api/[controller]")]
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

        public bool Transfer(int id, int accountNumberFrom, int accountNumberTo, double amount)
        {
            if (AuditTransaction(accountNumberFrom, amount) == true) 
            {
                //do the transaction here 
            }
            throw new NotImplementedException();
        }

        public bool Withdraw(int id,int accountNumber, double amount)
        {
            if (AuditTransaction(accountNumber, amount) == true)
            {
                bankDataAccessLayer.UpdateAfterWithdraw(id, accountNumber.ToString(), amount);
            }
            throw new NotImplementedException();
        }
    }
}
