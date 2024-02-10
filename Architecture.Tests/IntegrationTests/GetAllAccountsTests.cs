using Architecture.Domain.Models;
using Architecture.Impl.EFDatabase;
using Architecture.Impl.Repositories;
using Architecturee.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Architecture.Tests
{
    public class GettAllAccountsTests
    {
        [TestClass]
        public class OverdraftAccountRepositoryIntegrationTests
        {
            private Mock<AppDbContext> _mockContext;

            private AccountRepository _accountRepository;
            private CustomerRepository _customerRepository;

            private NoOverdraftAccountRepository _noOverdraftAccountRepository;
            private AccountController _overdraftAccountController;


            [TestInitialize]
            public void Init()
            {
                _mockContext = new Mock<AppDbContext>();
                _noOverdraftAccountRepository = new NoOverdraftAccountRepository(_mockContext.Object);
                _accountRepository = new AccountRepository(_mockContext.Object);
                _customerRepository = new CustomerRepository(_mockContext.Object);

                _overdraftAccountController = new AccountController(_accountRepository, _noOverdraftAccountRepository, _customerRepository);
            }

            [TestMethod]
            public void GetAccounts_Returns_OverdraftAccounts()
            {

                // Créer des données simulées à retourner par la propriété Accounts
                var accountsData = new List<Account>
                {
                    new OverdraftAccount(),
                    new OverdraftAccount()
                };

                // Créer un DbSet simulé à partir des données simulées
                var mockDbSet = new Mock<DbSet<Account>>();
                mockDbSet.As<IQueryable<Account>>().Setup(m => m.Provider).Returns(accountsData.AsQueryable().Provider);
                mockDbSet.As<IQueryable<Account>>().Setup(m => m.Expression).Returns(accountsData.AsQueryable().Expression);
                mockDbSet.As<IQueryable<Account>>().Setup(m => m.ElementType).Returns(accountsData.AsQueryable().ElementType);
                mockDbSet.As<IQueryable<Account>>().Setup(m => m.GetEnumerator()).Returns(() => accountsData.AsQueryable().GetEnumerator());

                // Configurer le comportement de la propriété Accounts
                _mockContext.SetupGet(c => c.Accounts).Returns(mockDbSet.Object);

                // Act
                var simulatedAccounts = _mockContext.Object.Accounts.ToList();

                var result = _overdraftAccountController.GetAccounts() as OkObjectResult;
                var returnedAccounts = result?.Value as List<Account>;

                // Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(StatusCodes.Status200OK, result.StatusCode);
                Assert.IsNotNull(returnedAccounts);
                Assert.AreEqual(simulatedAccounts.Count, returnedAccounts.Count);

            }

        }
    }
}
