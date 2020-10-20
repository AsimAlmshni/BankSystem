using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Models
{
    public class CheckingAccountFactory : BankAccountFactory
    {
        private string currency;
        private double balance;
        private int accountNumber;

        public CheckingAccountFactory(int accountNumber)
        {
            this.accountNumber = accountNumber;
        }
        public override Account GetBankAccountType()
        {
            return new CheckingAccount(accountNumber);
        }
    }
}
