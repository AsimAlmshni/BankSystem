using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Bank.Models
{
    public class BankDataAccessLayer
    {
        BankContext db = new BankContext();

        public dynamic GetAccountsBalance(int id ) {

            var tbl = from cust in db.Customer
                      join account in db.Accounts
                      on cust.CustomerId equals account.CustomerId
                      where (cust.CustomerId == id)
                      select new { accountBalance = account.Balance};
            return tbl.ToList();
        }

        public void UpdateAccountBalance(int id, string accountNumber, double amount) 
        {
            var result = (from cust in db.Customer
                         where (cust.CustomerId == id)
                         select cust).FirstOrDefault();
            result.TotalBalance += amount;

            var result2 = (from acc in db.Accounts
                          where (acc.CustomerId == id && acc.AccountNumber == accountNumber)
                          select acc).FirstOrDefault();
            result2.Balance += amount;

            db.SaveChanges();
        }

        public void UpdateAfterWithdraw(int id, string accountNumber, double amount) 
        {
            var result = (from cust in db.Customer
                          where (cust.CustomerId == id)
                          select cust).FirstOrDefault();
            result.TotalBalance -= amount;

            var result2 = (from acc in db.Accounts
                           where (acc.CustomerId == id && acc.AccountNumber == accountNumber)
                           select acc).FirstOrDefault();
            result2.Balance -= amount;
        }

        public void TransferBetweenAccounts(int id, int accountNumberFrom, int accountNumberTo, double amount) 
        {
            var temp = (from cust in db.Customer
                       join acc in db.Accounts
                       on cust.CustomerId equals acc.CustomerId
                       where (cust.CustomerId == id && acc.CustomerId == id && acc.AccountNumber == accountNumberFrom.ToString())
                       select acc).FirstOrDefault();

            temp.Balance -= amount;

            var temp2 = (from cust in db.Customer
                         join acc in db.Accounts
                         on cust.CustomerId equals acc.CustomerId
                         where (cust.CustomerId == id && acc.CustomerId == id && acc.AccountNumber == accountNumberTo.ToString())
                         select acc).FirstOrDefault();

            temp.Balance += amount;

            db.SaveChanges();
        }
        public IEnumerable<Customer> GetAllCustomers()
        {
            try
            {
                return db.Customer.ToList();
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<Currencies> GetAllCurrencies() 
        {
            try
            {
                return db.Currencies.ToList();
            }
            catch
            {
                throw;
            }
        }

        //To Add new customer record   
        public int AddCustomer(Customer customer)
        {
            try
            {
                var clntID = from client in db.Client select client.CliId;
                customer.ClientId = clntID.FirstOrDefault();
                customer.AccId = int.Parse(customer.MainAccountNumber);
                db.Customer.Add(customer);
                db.SaveChanges();
                return 1;
            }
            catch
            {
                throw;
            }
        }

        //To Update the records of a particluar customer  
        public int UpdateCustomer(Customer customer)
        {
            try
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();

                return 1;
            }
            catch
            {
                throw;
            }
        }

        //Get the details of a particular Customer  
        public Customer GetCustomerData(int id)
        {
            try
            {
                Customer customer = db.Customer.Find(id);
                return customer;
            }
            catch
            {
                throw;
            }
        }

        public string GetBankNameDB() {
            var bnkName = from bank in db.Bank select bank.BankName;
            return bnkName.FirstOrDefault().ToString();
        }

        //To Get the list of AccountsActions  
        public List<AccountActionHistory> GetHistory()
        {
            try
            {
                return db.AccountActionHistory.ToList();
            }
            catch
            {
                throw;
            }
        }
    }
}
