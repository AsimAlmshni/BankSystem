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
    public class CustomerController : ControllerBase
    {
        private BankDataAccessLayer obj = new BankDataAccessLayer();

        [HttpGet]
        public IEnumerable<Customer> GetCustomer()
        {
            return obj.GetAllCustomers();
        }

        [HttpGet]
        public IEnumerable<Currencies> GetCurrencies() 
        {
            return obj.GetAllCurrencies();
        }

        [HttpGet]
        public string GetAutoGenAccountNumber()
        {
            Bank.Models.Bank bank = Bank.Models.Bank.Instance;
            return bank.GetGeneratedNumber().ToString();
        }

        [HttpPost]
        public int Create([FromBody] Customer customer)
        {
            return obj.AddCustomer(customer);
        }

        [HttpGet]
        [Route("api/Customer/GetCustomer/{id}")]
        public Customer GetCustomer(int id)
        {
            return obj.GetCustomerData(id);
        }

        [HttpPut]
        [Route("api/Customer/Edit")]
        public int Edit([FromBody] Customer employee)
        {
            return obj.UpdateCustomer(employee);
        }
    }
}
