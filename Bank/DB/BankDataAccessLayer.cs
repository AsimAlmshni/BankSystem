using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Bank.Models
{
    internal class BankDataAccessLayer
    {
        BankContext db = new BankContext();
        DateTime now = DateTime.Now;


        public IEnumerable<Customer> GetEqualsCustomersAccount(string accountnumber) 
        {
            var allCustomers = from customer in db.Customer
                               where customer.MainAccountNumber == accountnumber
                               select customer;
              

            if (allCustomers.Count() >= 2)
            {
                return allCustomers.ToList();
            }
            return null;
        }

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
                            where cust.CustomerId == id && account.AccId == accAH.AccountsAccId
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


                var cust = (from cus in db.Customer
                            join acc in db.Accounts
                            on cus.CustomerId equals acc.CustomerId
                            select cus).FirstOrDefault();


                var xeCh = from cur in db.Currencies
                           where cur.Currency == result.Currency
                           select cur.ExchangeRate;

                var mainCurrencyExchange = from cur in db.Currencies
                                           where cur.Currency == cust.MainCurrency
                                           select cur.ExchangeRate;

                var xe = xeCh.FirstOrDefault() / mainCurrencyExchange.FirstOrDefault();
                cust.TotalBalance += xe * account.Amount;

                var result2 = db.Customer.SingleOrDefault(b => b.MainAccountNumber == cust.MainAccountNumber);
                if (result2 != null)
                {
                    result2.TotalBalance = cust.TotalBalance;
                }

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


                var cust = (from cus in db.Customer
                            join acc in db.Accounts
                            on cus.CustomerId equals acc.CustomerId
                              select cus).FirstOrDefault();


                var xeCh = from cur in db.Currencies
                           where cur.Currency == result.Currency
                           select cur.ExchangeRate;

                var mainCurrencyExchange = from cur in db.Currencies
                           where cur.Currency == cust.MainCurrency
                           select cur.ExchangeRate;

                var xe = xeCh.FirstOrDefault() / mainCurrencyExchange.FirstOrDefault();
                cust.TotalBalance -= xe * account.Amount;

                var result2 = db.Customer.SingleOrDefault(b => b.MainAccountNumber == cust.MainAccountNumber);
                if (result2 != null)
                {
                    result2.TotalBalance = cust.TotalBalance;
                }

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
            double amountEx = 0;
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

              amountEx = accountTransfer.Amount * tempEx;
            if (amountEx < tempAccFrom.Balance) {

                    tempAccFrom.Balance -= accountTransfer.Amount;

                    tempAccTo.Balance += amountEx;

                    accountTransfer.Date = now;
                    accountTransfer.AccountsAccId = tempAccFrom.AccId;
                    accountTransfer.ActionType = "transfer";
                    accountTransfer.Currency = tempAccFrom.Currency;

                    db.AccountActionHistory.Add(accountTransfer);
                    // Aduit records 
                
                    db.SaveChanges();
                  }
                }
                else if (accountTransfer.Amount < tempAccFrom.Balance)
                {

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
                return db.Customer.ToList();
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

                var mainCurrencyExchange = from curr in db.Currencies
                                   where curr.Currency == customer.customer.MainCurrency
                                   select curr.ExchangeRate;

                var customerID = from cust in db.Customer
                                 where cust.MainAccountNumber == customer.customer.MainAccountNumber
                                 select cust.CustomerId;


                foreach (var item in customer.accounts)
                {
                    var generatedAccountNumber = Bank.Instance.GetGeneratedNumber().ToString();
                    Accounts tempAaccount = new Accounts();
                    tempAaccount.Balance = item.Balance;
                    tempAaccount.Currency = item.Currency;
                    tempAaccount.CustomerId = customerID.FirstOrDefault();
                    tempAaccount.AccountNumber = generatedAccountNumber;

                    var xeCh = from cur in db.Currencies
                                where cur.Currency == item.Currency
                                select cur.ExchangeRate;

                    var xe = xeCh.FirstOrDefault() / mainCurrencyExchange.FirstOrDefault() ;
                    customer.customer.TotalBalance += xe * item.Balance;

                    var result = db.Customer.SingleOrDefault(b => b.MainAccountNumber == customer.customer.MainAccountNumber);
                    if (result != null)
                    {
                        result.TotalBalance = customer.customer.TotalBalance;
                    }

                    db.Accounts.Add(tempAaccount);
                    db.SaveChanges();

                    var accID = from acc in db.Accounts
                                where acc.CustomerId == customer.customer.CustomerId
                                && acc.AccountNumber == generatedAccountNumber
                                select acc.AccId;
                    var flag = 1;
                    var v1 = accID.FirstOrDefault();
                    
                    foreach (var typ in item.accountTypes)
                    {
                        AccountTypes accTyps = new AccountTypes();
                        accTyps.AccIdtyp = v1;
                        var genAcc = Bank.CreateAccount(typ.ToString());
                        accTyps.AccountType = genAcc.AccountType;
                        if(flag == 1)
                        {
                            generatedAccountNumber = genAcc.AccountNumber.ToString();
                            flag = 0;
                        }
                        db.AccountTypes.Add(accTyps);
                        db.SaveChanges();
                    }

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
                Customer customer = db.Customer.Find(id);
                return customer;
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
                return db.AccountActionHistory.ToList();
        }
    }
}
