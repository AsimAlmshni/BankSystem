using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Models
{
    interface ITransactions
    {
        public bool Deposite(int accountNumberFrom);
        public bool Transfer(int accountNumberFrom, int accountNumberTo);
        public bool Withdraw(int accountNumberFrom);
        public bool AuditTransaction(int accountNumberFrom);


    }
}
