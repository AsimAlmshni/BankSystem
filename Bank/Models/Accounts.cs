using System;
using System.Collections.Generic;

namespace Bank.Models
{
    public partial class Accounts
    {
        public Accounts()
        {
            AccountActionHistory = new HashSet<AccountActionHistory>();
            AccountTypes = new HashSet<AccountTypes>();
        }

        public int AccId { get; set; }
        public string AccountNumber { get; set; }
        public string Currency { get; set; }
        public int CustomerId { get; set; }
        public double Balance { get; set; }


        public virtual Customer Customer { get; set; }
        public virtual ICollection<AccountActionHistory> AccountActionHistory { get; set; }
        public virtual ICollection<AccountTypes> AccountTypes { get; set; }
    }
}
