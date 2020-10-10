using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Models
{
    public abstract class Account
    {
        public abstract string AccountType { get; }
        public abstract int AccountNumber{ get; set; }
        public abstract double Balance { get; set; }
        public abstract string Currency { get; set; }
        
    }
}
