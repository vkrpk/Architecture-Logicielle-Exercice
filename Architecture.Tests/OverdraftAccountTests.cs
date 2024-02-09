using Architecture.Domain.Models;
using Architecture.Impl.EFDatabase;
using Architecture.Impl.Repositories;
using Moq;

namespace Architecture.Tests
{
    [TestClass]
    public class OverdraftAccountTests
    {
        private OverdraftAccount _overdraftAccount;
        private Customer _customer;
        private OverdraftAccountRepository _mockOverdraftAccountRepository;
        private Mock<AppDbContext> _mockContext;

        [TestInitialize]
        public void Init()
        {
            _mockContext = new Mock<AppDbContext>();
            _mockOverdraftAccountRepository = new OverdraftAccountRepository(_mockContext.Object);
            _customer = new Customer();
            _overdraftAccount = new OverdraftAccount(_customer);
        }

        [TestMethod]
        public void CreditOverdraftAccountNominalTest()
        {
            var result = _mockOverdraftAccountRepository.Credit(100, _overdraftAccount);
            Assert.AreEqual(100, result);
        }

        [TestMethod]
        public void DebitOverdraftAccountNominalTest()
        {
            var result = _mockOverdraftAccountRepository.Debit(100, _overdraftAccount);
            Assert.AreEqual(-100, result);
        }

        [TestMethod]
        public void DebitOverdraftAccountNegativeBalanceTest()
        {
            _mockOverdraftAccountRepository.Credit(100, _overdraftAccount);
            var result = _mockOverdraftAccountRepository.Debit(100, _overdraftAccount);
            Assert.AreEqual(0, result);
        }
    }
}