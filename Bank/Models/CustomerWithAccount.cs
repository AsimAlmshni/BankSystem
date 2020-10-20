using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Bank.Models
{
    public class CustomerWithAccount
    {
        public Customer customer;

        public AccountForm[] accounts;
        //public AccountTypes[] accountTypes;

    }
}
