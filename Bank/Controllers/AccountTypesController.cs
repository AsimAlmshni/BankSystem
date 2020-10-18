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
    public class AccountTypesController : ControllerBase
    {
        private BankDataAccessLayer obj = new BankDataAccessLayer();

        [HttpGet("{id}")]
        public IEnumerable<AccountTypes> GetAccountTypes(int id)
        {
            return obj.GetAccountIdType(id);
        }

        [HttpGet]
        public IEnumerable<AccountTypesDataSet> getAccountsTypsDataSet() {
            return obj.GetAccountTypesDS();
        }
    }
}
