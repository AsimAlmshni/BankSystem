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
        public ClientController() {
            BankDataAccessLayer bankDataAccessLayer = new BankDataAccessLayer();
        }
        public bool AuditTransaction(int accountNumberFrom)
        {
            DateTime localDate = DateTime.Now;
            DateTime utcDate = DateTime.UtcNow;


            throw new NotImplementedException();
        }

        public bool Deposite(int accountNumberFrom)
        {
            throw new NotImplementedException();
        }

        public bool Transfer(int accountNumberFrom, int accountNumberTo)
        {
            throw new NotImplementedException();
        }

        public bool Withdraw(int accountNumberFrom)
        {
            throw new NotImplementedException();
        }
    }
}
