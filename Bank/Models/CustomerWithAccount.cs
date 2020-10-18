using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Models
{
    public class CustomerWithAccount
    {
        public Customer customer = new Customer();
        public Accounts accounts  = new Accounts();
        public AccountTypes accountTypes = new AccountTypes();

    }
}
