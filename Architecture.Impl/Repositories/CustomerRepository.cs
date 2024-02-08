using Architecture.Domain.Models;
using Architecture.Impl.EFDatabase;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Architecture.Impl.Repositories
{
    internal class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _context;

        public CustomerRepository(AppDbContext context)
        {
            _context = context;
        }

        public Customer createCustomer(Customer customer)
        {
            EntityEntry<Customer> createdCustomer = _context.Customers.Add(customer);
            _context.SaveChanges();

            return createdCustomer.Entity;
        }

        public string deleteCustomer(Guid customerId)
        {
            Customer customer = getCustomerById(customerId);
            _context.Customers.Remove(customer);
            _context.SaveChanges();

            return "Customer " + customerId + " supprimé avec succès";
        }

        public List<Customer> getAllCustomer()
        {
            return _context.Customers.ToList();
        }

        public Customer getCustomerByClientName(string clientName)
        {
            try
            {
                return _context.Customers.Include(a => a.Accounts)
                    .FirstOrDefault(a => a.Name == clientName);
            }
            catch (Exception ex)
            {
                throw new Exception($"Impossible de trouver le compte avec l'ID {clientName}.", ex);
            }
        }

        public Customer getCustomerById(Guid customerId)
        {
            try
            {
                return _context.Customers.Include(a => a.Accounts)
                    .FirstOrDefault(a => a.Id == customerId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Impossible de trouver le compte avec l'ID {customerId}.", ex);
            }
        }

        public Customer updateCustomer(Guid customerId, Customer customer)
        {
            EntityEntry<Customer> updatedCustomer = _context.Customers.Update(customer);
            _context.SaveChangesAsync();

            return updatedCustomer.Entity;
        }
    }
}
