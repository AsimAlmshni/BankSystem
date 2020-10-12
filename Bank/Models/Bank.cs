using System;
using System.Collections.Generic;

namespace Bank.Models
{
    public partial class Bank
    {       
        public int BankId { get; set; }
        public string BankName { get; set; }

        private int client;

        public int GetClient()
        {
            return client;
        }

        public void SetClient(int value)
        {
            client = value;
        }

        private static Bank instance = null;
        private Bank()
        {
        }
        public static Bank Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Bank();
                }
                return instance;
            }
        }

        public virtual ICollection<Client> Client { get; set; }

        public void CreateAccount(string Type, string currency, double balance) 
        {
            int accountNumber = Bank.AccountNumberGenerator();
            BankAccountFactory factory = null;

            switch (Type.ToLower())
            {
                case "deposite":
                    factory = new DepositeAccountFactory(currency, balance, accountNumber);
                    break;
                case "checking":
                    factory = new CheckingAccountFactory(currency, balance, accountNumber);
                    break;
                default:
                    break;
            }
        }

        // account number generator
        private static int AccountNumberGenerator() {
            Random rnd = new Random();
            int accountNumber = rnd.Next(10000000, 99999999);

            return accountNumber;
        }
    }
}
