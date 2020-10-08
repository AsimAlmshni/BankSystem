using System;
using System.Collections.Generic;

namespace Bank.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Accounts = new HashSet<Accounts>();
        }

        public int CustomerId { get; set; }
        public string MainAccountNumber { get; set; }
        public int AccId { get; set; }
        public string MainCurrency { get; set; }
        public double TotalBalance { get; set; }
        public string CustomerName { get; set; }
        public int ClientId { get; set; }

        public virtual Client Client { get; set; }
        public virtual ICollection<Accounts> Accounts { get; set; }
    }
}
