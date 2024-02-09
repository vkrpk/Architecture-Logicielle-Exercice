using Architecture.Domain.Models;
//using Architecture.Controllers; // Sauf si changement deffaultNamespace
using Architecture.Impl.EFDatabase;
using Architecture.Impl.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DefaultNamespace;

namespace Architecture.Tests
{
    public class GettAllAccountsTests
    {
        [TestClass]
        public class OverdraftAccountRepositoryIntegrationTests
        {
            private AccountRepository _noOverdraftAccount;
            private NoOverdraftAccountRepository _mockNoOverdraftAccountRepository;
            private CustomerRepository _customerRepository;

            private OverdraftAccount _overdraftAccount1;
            private OverdraftAccount _overdraftAccount2;
            private AccountController _overdraftAccountController;
            private Mock<AppDbContext> _mockContext;

            [TestInitialize]
            public void Init()
            {
                _mockContext = new Mock<AppDbContext>();
                _mockNoOverdraftAccountRepository = new NoOverdraftAccountRepository(_mockContext.Object);
                _noOverdraftAccount = new AccountRepository(_mockContext.Object);
                _customerRepository = new CustomerRepository(_mockContext.Object);

                _overdraftAccountController = new AccountController(_noOverdraftAccount, _mockNoOverdraftAccountRepository, _customerRepository);
            }

            [TestMethod]
            public void GetAccounts_Returns_OverdraftAccounts()
            {
                // Arrange
                var customer = new Customer();
                _overdraftAccount1 = new OverdraftAccount();
                _overdraftAccount2 = new OverdraftAccount();

                var overdraftAccountsList = new List<OverdraftAccount> { _overdraftAccount1, _overdraftAccount2 };


                var mockSet = new Mock<DbSet<OverdraftAccount>>();
                mockSet.As<IQueryable<OverdraftAccount>>().Setup(m => m.Provider).Returns(overdraftAccountsList.AsQueryable().Provider);
                mockSet.As<IQueryable<OverdraftAccount>>().Setup(m => m.Expression).Returns(overdraftAccountsList.AsQueryable().Expression);
                mockSet.As<IQueryable<OverdraftAccount>>().Setup(m => m.ElementType).Returns(overdraftAccountsList.AsQueryable().ElementType);
                mockSet.As<IQueryable<OverdraftAccount>>().Setup(m => m.GetEnumerator()).Returns(overdraftAccountsList.AsQueryable().GetEnumerator());


                _mockContext.Setup(c => c.OverdraftAccounts).Returns(mockSet.Object);

                // Act
                var result = _overdraftAccountController.GetAccounts();

                // Assert
                Assert.IsNotNull(result);
                Assert.IsTrue(result.Equals(2));
            }
        }
    }
}
