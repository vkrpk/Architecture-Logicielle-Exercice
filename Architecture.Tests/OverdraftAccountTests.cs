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
        private OverdraftAccountRepository _mockOverdraftAccountRepository;
        private Mock<AppDbContext> _mockContext;

        [TestInitialize]
        public void Init()
        {
            _mockContext = new Mock<AppDbContext>();
            _mockOverdraftAccountRepository = new OverdraftAccountRepository(_mockContext.Object);
            _overdraftAccount = new OverdraftAccount();
        }

        [TestMethod]
        public async Task CreditOverdraftAccountNominalTest()
        {
            var result = await _mockOverdraftAccountRepository.Credit(100, _overdraftAccount);
            Assert.AreEqual(100, result);
        }

        [TestMethod]
        public async Task DebitOverdraftAccountNominalTest()
        {
            var result = await _mockOverdraftAccountRepository.Debit(100, _overdraftAccount);
            Assert.AreEqual(-100, result);
        }

        [TestMethod]
        public async Task DebitOverdraftAccountNegativeBalanceTest()
        {
            await _mockOverdraftAccountRepository.Credit(100, _overdraftAccount);
            var result = await _mockOverdraftAccountRepository.Debit(100, _overdraftAccount);
            Assert.AreEqual(0, result);
        }
    }
}