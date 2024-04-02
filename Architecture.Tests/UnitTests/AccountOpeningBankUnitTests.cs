using Architecture.Domain.Models;
using Architecture.Impl.EFDatabase;
using Architecture.Impl.Repositories;
using Architecturee.Controllers;
using Castle.Core.Resource;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Architecture.Tests
{
    [TestClass]
    public class AccountOpeningBankUnitTests
    {

        private Mock<ICustomerRepository> _mockCustomerRepo;
        private Mock<IAccountRepository> _mockAccountRepo;
        private BankRepository _bankRepository;


        [TestInitialize]
        public void Init()
        {
            //_mockContext = new Mock<AppDbContext>();
            _mockAccountRepo = new Mock<IAccountRepository>();
            _mockCustomerRepo = new Mock<ICustomerRepository>();

            _bankRepository = new BankRepository(_mockAccountRepo.Object, _mockCustomerRepo.Object);

        }

        [TestMethod]
        public async Task AccountOpening_ValidClient_CreatesAccount()
        {
            // Arrange
            string clientName = "John Doe";
            bool isOverdraftAllowed = true;
            Customer customer = new Customer { Name = clientName };

            _mockCustomerRepo.Setup(x => x.getCustomerByClientName(clientName)).Returns(customer);

            // Act
            _bankRepository.AccountOpening(clientName, isOverdraftAllowed);

            // Assert
            _mockAccountRepo.Verify(x => x.createAccount(customer, isOverdraftAllowed), Times.Once);
            
        }

        [TestMethod]
        public void AccountOpening_InvalidClient_NoAccountCreated()
        {
            // Arrange
            string clientName = "Nonexistent Client";

            _mockCustomerRepo.Setup(x => x.getCustomerByClientName(clientName)).Returns((Customer)null);

            // Act
            _bankRepository.AccountOpening(clientName, true); // Overdraft allowed or not doesn't matter for this test

            // Assert
            _mockAccountRepo.Verify(x => x.createAccount(It.IsAny<Customer>(), It.IsAny<bool>()), Times.Never);
        }

        [TestMethod]
        public void Consultation_ValidAccount_ReturnsBalance()
        {
            // Arrange
            Guid accountNumber = Guid.NewGuid();
            int expectedBalance = 1000;
            Account account = new AccountConcreteImplementation { AccountNumber = accountNumber, Balance = expectedBalance };

            _mockAccountRepo.Setup(x => x.getAccountByNumber(accountNumber)).Returns(account);


            // Act
            int actualBalance = _bankRepository.Consultation(accountNumber);

            // Assert
            Assert.AreEqual(expectedBalance, actualBalance);
        }

        private class AccountConcreteImplementation : Account
        {
            // Implement required members for testing
        }

    }
}
