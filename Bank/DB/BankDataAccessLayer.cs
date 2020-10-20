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
        DateTime now = DateTime.Now;

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

        public void UpdateAccountBalance(AccountActionHistory account) 
        {
            try
            {
                var result = (from acc in db.Accounts
                             where (acc.AccountNumber == account.FromAccount.ToString())
                             select acc).FirstOrDefault();

                result.Balance += account.Amount;

                account.ToAccount = account.FromAccount;
                account.AccountsAccId = result.AccId;
                account.Date = now;
                account.ActionType = "deposite";
                account.Currency = result.Currency;

                db.AccountActionHistory.Add(account);

                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void UpdateAfterWithdraw(AccountActionHistory account) 
        {
            try
            {
                var result = (from acc in db.Accounts
                              where (acc.AccountNumber == account.FromAccount.ToString())
                              select acc).FirstOrDefault();

                result.Balance -= account.Amount;

                account.ToAccount = account.FromAccount;
                account.AccountsAccId = result.AccId;
                account.Date = now;
                account.ActionType = "withdraw";
                account.Currency = result.Currency;

                db.AccountActionHistory.Add(account);

                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void DoTransfer(AccountActionHistory accountTransfer) 
        {
            try
            {
              var tempAccFrom = (from account in db.Accounts
                      where (account.AccountNumber == accountTransfer.FromAccount)
                      select account).FirstOrDefault();
              var tempAccTo = (from account in db.Accounts
                                where (account.AccountNumber == accountTransfer.ToAccount)
                                select account).FirstOrDefault();

            if(tempAccFrom.Currency != tempAccTo.Currency)
            {
              var exChane = from exch in db.Currencies
                      where exch.Currency == tempAccFrom.Currency
                      select exch.ExchangeRate;
              var exCh = from ex in db.Currencies
                        where ex.Currency == tempAccTo.Currency
                        select ex.ExchangeRate;
              var tempEx = exChane.FirstOrDefault() / exCh.FirstOrDefault();

              accountTransfer.Amount = accountTransfer.Amount * tempEx;
            }
        if (accountTransfer.Amount < tempAccFrom.Balance) {

                tempAccFrom.Balance -= accountTransfer.Amount;

                tempAccTo.Balance += accountTransfer.Amount;

                accountTransfer.Date = now;
                accountTransfer.AccountsAccId = tempAccFrom.AccId;
                accountTransfer.ActionType = "transfer";
                accountTransfer.Currency = tempAccFrom.Currency;

                db.AccountActionHistory.Add(accountTransfer);
                // Aduit records 
                
                db.SaveChanges();
              }
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }
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

                var clntID = from client in db.Client 
                             select client.CliId;
                customer.customer.ClientId = clntID.FirstOrDefault();

                //customer.AccId = int.Parse(customer.MainAccountNumber);
                db.Customer.Add(customer.customer);
                db.SaveChanges();

                var customerID = from cust in db.Customer
                                 where cust.MainAccountNumber == customer.customer.MainAccountNumber
                                 select cust.CustomerId;

                var generatedAccountNumber = Bank.Instance.GetGeneratedNumber().ToString();
                foreach (var item in customer.accounts)
                {
                    item.CustomerId = customerID.FirstOrDefault();
                    item.AccountNumber = generatedAccountNumber;
                    db.Accounts.Add(item);
                    db.SaveChanges();

                    var accID = from acc in db.Accounts
                                where acc.CustomerId == customer.customer.CustomerId
                                select acc.AccId;

                    foreach (var itemType in customer.accountTypes)
                    {
                        AccountTypes accTyps = new AccountTypes();
                        var v1 = accID.FirstOrDefault();
                        accTyps.AccIdtyp = v1;
                        var genAcc = Bank.CreateAccount(itemType.ToString());
                        accTyps.AccountType = genAcc.AccountType;
                        generatedAccountNumber = genAcc.AccountNumber.ToString();
                        db.AccountTypes.Add(accTyps);
                    }
                    db.SaveChanges();
                }




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

        public IEnumerable<Bank> GetBankNameDB() {
            var bnkName = from bank in db.Bank select bank;
            return bnkName.ToList();
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
