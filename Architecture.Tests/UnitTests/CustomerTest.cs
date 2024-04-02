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
    private Mock<ICustomerRepository> _mockCustomerRepo;

    [TestInitialize]
    public void Init()
    {
        _mockContext = new Mock<AppDbContext>();
        var mockSet = new Mock<DbSet<Customer>>();
        _mockContext.Setup(m => m.Customers).Returns(mockSet.Object);
        _mockCustomerRepo = new Mock<ICustomerRepository>();
    }

    [TestMethod]
    public void CreateCustomerNominalTest()
    {
        var customer = new Customer();

        _mockCustomerRepo.Object.createCustomer(customer);

        _mockCustomerRepo.Verify(x => x.createCustomer(customer), Times.Once);

    }
}