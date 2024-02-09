using Architecture.Domain.Models;
using Architecture.Impl.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Architecture.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class CustomerController : Controller
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerController(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    [HttpGet("{customerId}")]
    public IActionResult getCustomerById(Guid customerId)
    {
        Customer customer = _customerRepository.getCustomerById(customerId);

        if (customer == null)
        {
            return NotFound();
        }

        return Ok(customer);
    }

    [HttpGet("{clientName}")]
    public IActionResult getCustomerByName(string clientName)
    {
        Customer customer = _customerRepository.getCustomerByClientName(clientName);

        if (customer == null)
        {
            return NotFound();
        }

        return Ok(customer);
    }

    [HttpGet]
    public IActionResult getAllCustomers()
    {
        List<Customer> customerArray = _customerRepository.getAllCustomer();

        if (customerArray == null)
        {
            return NotFound();
        }

        return Ok(customerArray);
    }

    [HttpPost]
    public IActionResult createCustomer(Customer customer)
    {
        Customer 
            _customer = _customerRepository.createCustomer(customer);

        if (_customer == null)
        {
            return NotFound();
        }

        return Ok(_customer);
    }

    [HttpPut("{customerId}")]
    public IActionResult updateCustomer(Customer customer)
    {
        Customer _customer = _customerRepository.updateCustomer(customer);

        if (customer == null)
        {
            return NotFound();
        }

        return Ok(_customer);
    }

    [HttpDelete("{customerId}")]
    public IActionResult deleteCustomer(Guid customerId)
    {
        string deletedCustomer = _customerRepository.deleteCustomer(customerId);

        if (deletedCustomer == null)
        {
            return NotFound();
        }

        return Ok(deletedCustomer);
    }



}