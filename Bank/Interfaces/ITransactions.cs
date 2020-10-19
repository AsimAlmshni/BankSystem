using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Models
{
    interface ITransactions
    {
        public void Deposite(int id, int accountNumber, double amount);
        public void Transfer(AccountActionHistory accountTransfer);
        public void Withdraw(int id, int accountNumberFrom, double aamount);
        public bool AuditTransaction(int accountNumber, double amount);


    }
}
