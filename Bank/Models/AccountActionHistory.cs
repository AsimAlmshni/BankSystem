using System;
using System.Collections.Generic;

namespace Bank.Models
{
    public partial class AccountActionHistory
    {
        public int Aahid { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public string ActionType { get; set; }
        public string FromAccount { get; set; }
        public string ToAccount { get; set; }
        public string Currency { get; set; }
        public int AccountsAccId { get; set; }

        public virtual Accounts AccountsAcc { get; set; }
    }
}
