using Architecture.Domain.Models;
using Architecture.Impl.EFDatabase;
using Architecture.Impl.Repositories;
using Moq;

namespace Architecture.Tests
{
    [TestClass]
    public class OverdraftAccountUnitTests
    {
        private OverdraftAccount _overdraftAccount;
        private OverdraftAccountRepository _overdraftAccountRepository;
        private Mock<AppDbContext> _mockContext;

        [TestInitialize]
        public void Init()
        {
            _mockContext = new Mock<AppDbContext>();
            _overdraftAccountRepository = new OverdraftAccountRepository(_mockContext.Object);
            _overdraftAccount = new OverdraftAccount();
        }

        [TestMethod]
        public void CreditOverdraftAccountNominalTest()
        {
            var result = _overdraftAccountRepository.Credit(100, _overdraftAccount);
            Assert.AreEqual(100, result);
        }

        [TestMethod]
        public void DebitOverdraftAccountNominalTest()
        {
            var result = _overdraftAccountRepository.Debit(100, _overdraftAccount);
            Assert.AreEqual(-100, result);
        }

        [TestMethod]
        public void DebitOverdraftAccountNegativeBalanceTest()
        {
            _overdraftAccountRepository.Credit(100, _overdraftAccount);
            var result = _overdraftAccountRepository.Debit(100, _overdraftAccount);
            Assert.AreEqual(0, result);
        }
    }
}