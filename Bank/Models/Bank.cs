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

        public static Account CreateAccount(string type) 
        {
            int accountNumber = Bank.AccountNumberGenerator();
            BankAccountFactory factory = null;

            switch (type.ToLower())
            {
                case "deposit":
                    factory = new DepositeAccountFactory(accountNumber);
                    break;
                case "checking":
                    factory = new CheckingAccountFactory(accountNumber);
                    break;
                case "current":
                    factory = new CheckingAccountFactory(accountNumber);
                    break;
                case "saving":
                    factory = new CheckingAccountFactory(accountNumber);
                    break;
                default:
                    break;
            }
            return factory.GetBankAccountType();
        }

        public int GetGeneratedNumber() {
            return AccountNumberGenerator();
        }
        // account number generator
        private static int AccountNumberGenerator() {
            Random rnd = new Random();
            int accountNumber = rnd.Next(10000000, 99999999);

            return accountNumber;
        }
    }
}
