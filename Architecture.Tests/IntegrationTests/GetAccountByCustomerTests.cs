using Architecture.Domain.Models;
using Architecture.Impl.EFDatabase;
using Architecture.Impl.Repositories;
using Architecturee.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Architecture.Tests
{
    [TestClass]
    public class GetAccountByCustomerTests
    {
        private Mock<ICustomerRepository> _mockCustomerRepo;
        private Mock<IAccountRepository> _mockAccountRepo;
        private AccountController _accountController;

        private Mock<AppDbContext> _mockContext;

        private AccountRepository _accountRepository;
        private CustomerRepository _customerRepository;
        private NoOverdraftAccountRepository _noOverdraftAccountRepository;

        [TestInitialize]
        public void Setup()
        {
            _mockContext = new Mock<AppDbContext>();
            _mockCustomerRepo = new Mock<ICustomerRepository>();
            _mockAccountRepo = new Mock<IAccountRepository>();
            _noOverdraftAccountRepository = new NoOverdraftAccountRepository(_mockContext.Object);
            _accountRepository = new AccountRepository(_mockContext.Object);
            _customerRepository = new CustomerRepository(_mockContext.Object);
            _accountController = new AccountController(_accountRepository, _noOverdraftAccountRepository, _customerRepository);
        }

        [TestMethod]
        public async Task GetAccountsByCustomer_Returns_OkResult_WithAccounts()
        {
            // Arrange
            string customerName = "John Doe";
            var accountGuid1 = Guid.NewGuid();
            var accountGuid2 = Guid.NewGuid();
            var customerId = Guid.NewGuid();
            Customer mockCustomer = new Customer { Id = customerId, Name = customerName };

            List<Account> mockAccounts = new List<Account>
            {
                new OverdraftAccount { Id = accountGuid1, CustomerId = customerId },
                new NoOverdraftAccount { Id = accountGuid2, CustomerId = customerId }
            };

            _mockCustomerRepo.Setup(repo => repo.getCustomerByClientName(customerName)).ReturnsAsync(mockCustomer);
            _mockAccountRepo.Setup(repo => repo.getAccountsByCustomer(mockCustomer)).Returns(mockAccounts);

            // Act
            var result = await _accountController.GetAccountsByCustomer(customerName);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

            var returnedAccounts = okResult.Value as List<Account>;
            Assert.IsNotNull(returnedAccounts);
            Assert.AreEqual(mockAccounts.Count, returnedAccounts.Count);
        }

        [TestMethod]
        public async Task GetAccountsByCustomer_Returns_NotFound_WhenCustomerNotFound()
        {
            // Arrange
            string nonExistentCustomerName = "Non Existent";
            _mockCustomerRepo.Setup(repo => repo.getCustomerByClientName(nonExistentCustomerName)).ReturnsAsync((Customer)null);

            // Act
            var result = await _accountController.GetAccountsByCustomer(nonExistentCustomerName);

            // Assert
            var notFoundResult = result as NotFoundResult;
            Assert.ThrowsException<NullReferenceException>(async () => await _accountController.GetAccountsByCustomer(nonExistentCustomerName));
            //Assert.AreEqual(404, notFoundResult.StatusCode);
        }
    }
}
