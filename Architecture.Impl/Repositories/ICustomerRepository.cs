using Architecture.Domain.Models;

namespace Architecture.Impl.Repositories
{
    public interface ICustomerRepository
    {
        Task<Customer> getCustomerById(Guid customerId);
        Task<Customer> getCustomerByClientName(string clientName);
        Task<List<Customer>> getAllCustomer();
        Task<Customer> createCustomer(Customer customer);
        Task<Customer> updateCustomer(Customer customer);
        Task<string> deleteCustomer(Guid customerId);
    }
}
