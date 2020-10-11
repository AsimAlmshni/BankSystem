using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Models
{
    interface ITransactions
    {
        public bool Deposite(int id, int accountNumber, double amount);
        public bool Transfer(int id, int accountNumberFrom, int accountNumberTo, double amount);
        public bool Withdraw(int id, int accountNumberFrom, double aamount);
        public bool AuditTransaction(int accountNumber, double amount);


    }
}
