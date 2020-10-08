using System;
using System.Collections.Generic;

namespace Bank.Models
{
    public partial class Bank
    {
        private static Bank instance = null;
        private Bank()
        {

        }


        public int BankId { get; set; }
        public string BankName { get; set; }

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
    }
}
