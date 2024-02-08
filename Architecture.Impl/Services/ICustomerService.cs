using System;
using Architecture.Domain.Models;
using Architecture.Impl.Repositories;
using Microsoft.Identity.Client;

namespace Architecture.Impl.Services
{
    public interface ICustomerService
    {

        Customer getCustomerById(Guid customerId);
        Customer createCustomer(Customer customer);
        Customer updateCustomer(Guid customerId, Customer customer);
        string deleteCustomer(Guid customerId);
    }
}
