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
    public class AccountActionHistoryController : ControllerBase
    {
        private BankDataAccessLayer obj = new BankDataAccessLayer();

        [HttpGet("{id}")]
        public IEnumerable<AccountActionHistory> GetCustomerTransHistory(int id) 
        {
            return obj.GetCustomerAccountTransactionHistory(id);
        }
    }
}
