using System;
using System.Collections.Generic;

namespace Bank.Models
{
    public partial class Client
    {
        public Client()
        {
            Customer = new HashSet<Customer>();
        }

        public int CliId { get; set; }
        public int BankId { get; set; }

        public virtual Bank Bank { get; set; }
        public virtual ICollection<Customer> Customer { get; set; }
    }
}
