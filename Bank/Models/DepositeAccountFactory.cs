﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Bank.Models
{
    public class DepositeAccountFactory : BankAccountFactory
    {
        private string currency;
        private double balance;
        private int accountNumber;

        public DepositeAccountFactory(string currency, double balance, int accountNumber) {
            this.currency = currency;
            this.balance = balance;
            this.accountNumber = accountNumber;
        }
        public override Account GetBankAccountType()
        {
            return new DepositeAccount(currency, balance, accountNumber);
        }
    }
}
