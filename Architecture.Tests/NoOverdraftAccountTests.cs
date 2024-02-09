using Architecture.Domain.Models;
using Architecture.Impl.EFDatabase;
using Architecture.Impl.Repositories;
using Moq;

namespace Architecture.Tests
{
    [TestClass]
    public class NoOverdraftAccountTests
    {
        private NoOverdraftAccount _noOverdraftAccount;
        private NoOverdraftAccountRepository _mockNoOverdraftAccountRepository;
        private Mock<AppDbContext> _mockContext;

        [TestInitialize]
        public void Init()
        {
            _mockContext = new Mock<AppDbContext>();
            _mockNoOverdraftAccountRepository = new NoOverdraftAccountRepository(_mockContext.Object);
            _noOverdraftAccount = new NoOverdraftAccount();
        }

        [TestMethod]
        public async Task CreditNoOverdraftAccountNominalTest()
        {
            var result = await _mockNoOverdraftAccountRepository.Credit(100, _noOverdraftAccount);
            Assert.AreEqual(100, result);
        }

        [TestMethod]
        public async Task DebitNoOverdraftAccountNominalTest()
        {
            await _mockNoOverdraftAccountRepository.Credit(100, _noOverdraftAccount);

            var result = await _mockNoOverdraftAccountRepository.Debit(100, _noOverdraftAccount);

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void DebitNoOverdraftAccountNegativeBalanceTest()
        {
            Assert.ThrowsException<NoOverdraftException>(() => _mockNoOverdraftAccountRepository.Debit(100, _noOverdraftAccount));
        }
    }
}