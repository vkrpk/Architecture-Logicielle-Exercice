using Architecture.Domain.Models;

namespace Architecture.Impl.Repositories
{
    public interface ICustomerRepository
    {
        Customer getCustomerById(Guid customerId);
        Customer getCustomerByClientName(string clientName);
        List<Customer> getAllCustomer();
        Customer createCustomer(Customer customer);
        Customer updateCustomer(Customer customer);
        string  deleteCustomer(Guid customerId);
    }
}
