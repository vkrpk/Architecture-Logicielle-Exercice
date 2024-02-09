using Architecture.Domain.Models;
using Architecture.Impl.EFDatabase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Architecture.Impl.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _context;

        public CustomerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Customer> createCustomer(Customer customer)
        {
            EntityEntry<Customer> createdCustomer = _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return createdCustomer.Entity;
        }

        public async Task<string> deleteCustomer(Guid customerId)
        {
            Customer customer = await getCustomerById(customerId);
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return "Customer " + customerId + " supprimé avec succès";
        }

        public async Task<List<Customer>> getAllCustomer()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer> getCustomerByClientName(string clientName)
        {
            try
            {
                return _context.Customers.Include(a => a.Accounts)
                    .First(a => a.Name == clientName);
            }
            catch (Exception ex)
            {
                throw new Exception($"Impossible de trouver le compte avec l'ID {clientName}.", ex);
            }
        }

        public async Task<Customer> getCustomerById(Guid customerId)
        {
            try
            {
                return await _context.Customers.Include(a => a.Accounts)
                    .FirstAsync(a => a.Id == customerId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Impossible de trouver le compte avec l'ID {customerId}.", ex);
            }
        }

        public async Task<Customer> updateCustomer(Customer customer)
        {
            var entity = await _context.Customers.FindAsync(customer.Id);
            _context.Entry(entity).CurrentValues
                .SetValues(customer);

            EntityEntry<Customer> updatedCustomer = _context.Customers.Update(customer);
            await _context.SaveChangesAsync();

            return updatedCustomer.Entity;
        }
    }
}
