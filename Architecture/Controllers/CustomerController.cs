using Architecture.Domain.Models;
using Architecture.Impl.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;

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
    public IActionResult GetCustomerById(Guid customerId)
    {
        Customer customer = _customerRepository.getCustomerById(customerId);

        if (customer == null)
        {
            return NotFound();
        }
        var options = new JsonSerializerOptions
        {
            ReferenceHandler = ReferenceHandler.Preserve,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

        string json = JsonSerializer.Serialize(customer, options);

        return Ok(json);

    }

    [HttpGet("byName/{clientName}")]
    public IActionResult GetCustomerByName(string clientName)
    {
        Customer customer = _customerRepository.getCustomerByClientName(clientName);

        if (customer == null)
        {
            return NotFound();
        }

        var options = new JsonSerializerOptions
        {
            ReferenceHandler = ReferenceHandler.Preserve,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

        string json = JsonSerializer.Serialize(customer, options);

        return Ok(json);
    }

    [HttpGet]
    public IActionResult GetAllCustomers()
    {
        List<Customer> customerArray = _customerRepository.getAllCustomer();

        if (customerArray == null)
        {
            return NotFound();
        }

        return Ok(customerArray);
    }

    [HttpPost]
    public IActionResult CreateCustomer(Customer customer)
    {
        Customer newCustomer = _customerRepository.createCustomer(customer);

        if (newCustomer == null)
        {
            return NotFound();
        }

        var options = new JsonSerializerOptions
        {
            ReferenceHandler = ReferenceHandler.Preserve,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

        string json = JsonSerializer.Serialize(newCustomer, options);

        return Ok(json);
    }

    [HttpPut("{customerId}")]
    public IActionResult UpdateCustomer(Customer customer)
    {
        Customer newCustomer = _customerRepository.updateCustomer(customer);

        if (customer == null)
        {
            return NotFound();
        }

        var options = new JsonSerializerOptions
        {
            ReferenceHandler = ReferenceHandler.Preserve,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

        string json = JsonSerializer.Serialize(newCustomer, options);

        return Ok(json);
    }

    [HttpDelete("{customerId}")]
    public IActionResult DeleteCustomer(Guid customerId)
    {
        string deletedCustomer = _customerRepository.deleteCustomer(customerId);

        if (deletedCustomer == null)
        {
            return NotFound();
        }

        var options = new JsonSerializerOptions
        {
            ReferenceHandler = ReferenceHandler.Preserve,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

        string json = JsonSerializer.Serialize(deletedCustomer, options);

        return Ok(json);
    }



}