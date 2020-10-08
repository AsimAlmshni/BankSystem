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
    public class CustomerController : ControllerBase
    {
        BankDataAccessLayer obj = new BankDataAccessLayer();

        [HttpGet]
        [Route("api/Customer/Index")]
        public IEnumerable<Customer> Index()
        {
            return obj.GetAllCustomers();
        }

        [HttpPost]
        [Route("api/Customer/Create")]
        public int Create([FromBody] Customer customer)
        {
            return obj.AddCustomer(customer);
        }

        [HttpGet]
        [Route("api/Customer/Details/{id}")]
        public Customer Details(int id)
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
