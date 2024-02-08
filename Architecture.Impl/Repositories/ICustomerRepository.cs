using Architecture.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Architecture.Impl.Repositories
{
    internal interface ICustomerRepository
    {
        Customer getCustomerById(Guid customerId);
        Customer getCustomerByClientName(string clientName);
        List<Customer> getAllCustomer();
        Customer createCustomer(Customer customer);
        Customer updateCustomer(Guid customerId, Customer customer);
        string deleteCustomer(Guid customerId);
    }
}
