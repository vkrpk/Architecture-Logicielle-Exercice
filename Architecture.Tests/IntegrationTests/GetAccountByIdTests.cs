using Architecture.Domain.Models;
using Architecture.Impl.EFDatabase;
using Architecture.Impl.Repositories;
using Architecturee.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Architecture.Tests
{
    [TestClass]
    public class GetAccountByIdTests
    {
        private Mock<ICustomerRepository> _mockCustomerRepo;
        private Mock<IAccountRepository> _mockAccountRepo;
        private AccountController _accountController;

        private Mock<AppDbContext> _mockContext;

        private AccountRepository _accountRepository;
        private CustomerRepository _customerRepository;
        private NoOverdraftAccountRepository _noOverdraftAccountRepository;


        private string customerName = "John Doe";
        private Guid accountGuid1 = Guid.NewGuid();
        private Guid accountGuid2 = Guid.NewGuid();
        private Guid customerId = Guid.NewGuid();

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
        public async Task GetAccountById_Returns_OkResult_WithAccounts()
        {
            // Arrange
            Customer mockCustomer = new Customer { Id = customerId, Name = customerName };
            Account mockAccount = new OverdraftAccount { Id = accountGuid1, CustomerId = customerId }; // Supposons que c'est le compte que vous souhaitez retourner

            _mockCustomerRepo.Setup(repo => repo.getCustomerByClientName(customerName)).Returns(mockCustomer);
            _mockAccountRepo.Setup(repo => repo.getAccountById(accountGuid1)).Returns(mockAccount);

            // Act
            var result = _accountController.GetAccountById(accountGuid1);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

            var returnedAccount = okResult.Value as Account;
            Assert.IsNotNull(returnedAccount);
            Assert.AreEqual(mockAccount.Id, returnedAccount.Id);
        }

        [TestMethod]
        public async Task GetAccountById_Returns_NotFound_WhenCustomerNotFound()
        {
            _mockAccountRepo.Setup(repo => repo.getAccountById(It.IsAny<Guid>())).Throws(new Exception("Account not found"));

            // Act
            IActionResult result = null;
            try
            {
                result = _accountController.GetAccountById(Guid.NewGuid());
            }
            catch (Exception ex)
            {
                // Assert
                Assert.IsInstanceOfType(ex, typeof(Exception)); 
                return;
            }

            // Si aucune exception n'est lancée, le test échoue
            Assert.Fail("Expected exception was not thrown.");
        }

    }
}
