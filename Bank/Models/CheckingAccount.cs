using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Models
{
    public class CheckingAccount : Account
    {
        private readonly string accountType;
        private string currency;
        private double balance;
        private int accountNumber;

        public CheckingAccount(string currency, double balance, int accountNumber) {
            this.accountType = "Checking";
            this.currency = currency;
            this.balance = balance;
            this.accountNumber = accountNumber;
        }

        public override string AccountType { get { return accountType; } }
        public override int AccountNumber { get => accountNumber; set => this.accountNumber = value; }
        public override double Balance { get => balance; set => this.balance = value; }
        public override string Currency { get => currency; set => currency = value; }
    }
}
