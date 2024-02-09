using Architecture.Domain.Models;

namespace Architecture.Impl.Repositories
{
    public interface ICustomerRepository
    {
        Customer getCustomerById(Guid customerId);
        Customer getCustomerByName(string customerName);
        Customer getCustomerByClientName(string clientName);
        List<Customer> getAllCustomer();
        Customer createCustomer(Customer customer);
        Customer updateCustomer(Guid customerId, Customer customer);
        string deleteCustomer(Guid customerId);
    }
}
