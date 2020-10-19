using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Models
{
    interface ITransactions
    {
        public void Deposite(AccountActionHistory accountDeposite);
        public void Transfer(AccountActionHistory accountTransfer);
        public void Withdraw(AccountActionHistory accountWithdraw);
    }
}
