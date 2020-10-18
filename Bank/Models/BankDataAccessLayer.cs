using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Bank.Models
{
    internal class BankDataAccessLayer
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

        public IEnumerable<AccountTypes> GetAccountIdType(int id) 
        {
            return (from cust in db.Customer
                    join account in db.Accounts
                    on cust.CustomerId equals account.CustomerId
                    join accTps in db.AccountTypes
                    on account.AccId equals accTps.AccIdtyp
                    where cust.CustomerId == id
                    select accTps).ToList();
        }

        public IEnumerable<AccountActionHistory> GetCustomerAccountTransactionHistory(int id) 
        {
            var accActHis = from cust in db.Customer
                            join account in db.Accounts
                            on cust.CustomerId equals account.CustomerId
                            join accAH in db.AccountActionHistory
                            on account.AccId equals accAH.AccountsAccId
                            where cust.CustomerId == id
                            select accAH;
            return accActHis.ToList();

        }

        public IEnumerable<AccountActionHistory> GetTransactions() 
        {
            return from accTransHis in db.AccountActionHistory select accTransHis;
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

        public IEnumerable<Accounts> GetCustomerAccounts(int CustomerID) 
        {
            var result = (from account in db.Accounts where account.CustomerId == CustomerID select account).ToList();
            return  result;
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
        public int AddCustomer(CustomerWithAccount customer)
        {
            try
            {
                AccountTypes accTyps = new AccountTypes();

                var clntID = from client in db.Client 
                             select client.CliId;
                customer.customer.ClientId = clntID.FirstOrDefault();

                //customer.AccId = int.Parse(customer.MainAccountNumber);
                db.Customer.Add(customer.customer);
                db.SaveChanges();

                var customerID = from cust in db.Customer
                                 where cust.MainAccountNumber == customer.customer.MainAccountNumber
                                 select cust.CustomerId;

                customer.accounts.CustomerId = customerID.FirstOrDefault();
                db.Accounts.Add(customer.accounts);
                db.SaveChanges();

                var accID = from acc in db.Accounts
                            where acc.CustomerId == customer.customer.CustomerId
                            select acc.AccId;


                foreach (var item in customer.accountTypes)
                {
                    var v1 = accID.FirstOrDefault();
                    accTyps.AccIdtyp = v1;
                    accTyps.AccountType = item;
                    db.AccountTypes.Add(accTyps);
                }


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

        public IEnumerable<AccountTypesDataSet> GetAccountTypesDS() {
            return (from accTypDS in db.AccountTypesDataSet select accTypDS).ToList();
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
