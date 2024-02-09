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
    public async Task<IActionResult> getCustomerById(Guid customerId)
    {
        Customer customer = await _customerRepository.getCustomerById(customerId);

        if (customer == null)
        {
            return NotFound();
        }

        return Ok(customer);
    }

    [HttpGet("{clientName}")]
    public async Task<IActionResult> getCustomerByName(string clientName)
    {
        Customer customer = await _customerRepository.getCustomerByClientName(clientName);

        if (customer == null)
        {
            return NotFound();
        }

        return Ok(customer);
    }

    [HttpGet]
    public async Task<IActionResult> getAllCustomers()
    {
        List<Customer> customerArray = await _customerRepository.getAllCustomer();

        if (customerArray == null)
        {
            return NotFound();
        }

        return Ok(customerArray);
    }

    [HttpPost]
    public async Task<IActionResult> createCustomer(Customer customer)
    {
        Customer _customer = await _customerRepository.createCustomer(customer);

        if (_customer == null)
        {
            return NotFound();
        }

        return Ok(_customer);
    }

    [HttpPut("{customerId}")]
    public async Task<IActionResult> updateCustomer(Customer customer)
    {
        Customer _customer = await _customerRepository.updateCustomer(customer);

        if (customer == null)
        {
            return NotFound();
        }

        return Ok(_customer);
    }

    [HttpDelete("{customerId}")]
    public async Task<IActionResult> deleteCustomer(Guid customerId)
    {
        string deletedCustomer = await _customerRepository.deleteCustomer(customerId);

        if (deletedCustomer == null)
        {
            return NotFound();
        }

        return Ok(deletedCustomer);
    }



}