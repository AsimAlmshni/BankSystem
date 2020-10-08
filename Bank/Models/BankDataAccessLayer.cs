using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Models
{
    public class BankDataAccessLayer
    {
        BankContext db = new BankContext();

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

        //To Add new customer record   
        public int AddCustomer(Customer customer)
        {
            try
            {
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
