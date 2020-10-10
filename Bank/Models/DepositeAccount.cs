using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Models
{
    public class DepositeAccount : Account
    {
        private readonly string accountType;
        private string currency;
        private double balance;
        private int accountNumber;

        public DepositeAccount(string currency, double balance, int accountNumber) {
            this.accountType = "Deposite";
            this.currency = currency;
            this.balance = balance;
            this.accountNumber = accountNumber;
        }

        public override string AccountType => accountType;
        public override int AccountNumber { get => accountNumber; set => this.accountNumber = value; }
        public override double Balance { get => balance; set => this.balance = value; }
        public override string Currency { get => currency; set => this.currency = value; }
    }
}
