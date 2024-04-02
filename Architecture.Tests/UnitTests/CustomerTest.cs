using Architecture.Domain.Models;
using Architecture.Impl.EFDatabase;
using Architecture.Impl.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;

namespace Architecture.Tests;

[TestClass]
public class CustomerTest
{
    private Mock<AppDbContext> _mockContext;
    private CustomerRepository _mockCustomeRepository;

    [TestInitialize]
    public void Init()
    {
        _mockContext = new Mock<AppDbContext>();
        var mockSet = new Mock<DbSet<Customer>>();
        _mockContext.Setup(m => m.Customers).Returns(mockSet.Object);
        _mockCustomeRepository = new CustomerRepository(_mockContext.Object);
    }

    [TestMethod]
    public void CreateCustomerNominalTest()
    {
        var customer = new Customer();

        var result = _mockCustomeRepository.createCustomer(customer);

        Assert.IsInstanceOfType(result, typeof(Customer));
    }

    // public void DeleteCustomerNominalTest()
    // {
    //     // Arrange
    //     var customerId = Guid.NewGuid();
    //     var mockContext = new Mock<AppDbContext>();
    //     var customerRepository = new CustomerRepository(mockContext.Object);
    //
    //     // Act
    //     var result = customerRepository.deleteCustomer(customerId);
    //
    //     // Assert
    //     Assert.IsNotNull(result);
    // }
}