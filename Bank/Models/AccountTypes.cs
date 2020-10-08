using System;
using System.Collections.Generic;

namespace Bank.Models
{
    public partial class AccountTypes
    {
        public int AccTypId { get; set; }
        public string AccountType { get; set; }
        public int AccIdtyp { get; set; }

        public virtual Accounts AccIdtypNavigation { get; set; }
    }
}
