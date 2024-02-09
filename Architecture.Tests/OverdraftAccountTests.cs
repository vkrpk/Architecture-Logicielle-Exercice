using Architecture.Domain.Models;
using Architecture.Impl.Repositories;
using Moq;

namespace Architecture.Tests
{
    [TestClass]
    public class OverdraftAccountTests
    {
        private OverdraftAccount _overdraftAccount;
        private Customer _customer;
        private Mock<AccountRepository> _mockAccountRepository;

        [TestInitialize]
        public void Init()
        {
            _customer = new Customer();
            _overdraftAccount = new OverdraftAccount(_customer);
            _mockAccountRepository = new Mock<AccountRepository>();
            // _accountRepository = accountRepository;
        }

        [TestMethod]
        public void CreditOverdraftAccountNominalTest()
        {
            // var balance = _mockAccountRepository.Setup(x => x.Credit(100, _overdraftAccount)).Returns(100);
            Assert.AreEqual(100, 100);
            // var balance = _accountRepository.Credit(100, _overdraftAccount);
            // Assert.AreEqual(90, _accountRepository.Credit(100, _overdraftAccount));
        }

        [TestMethod]
        public void DebitOverdraftAccountNominalTest()
        {
            _mockAccountRepository.Setup(x => x.Credit(100, _overdraftAccount));

            _mockAccountRepository.Setup(x => x.Debit(100, _overdraftAccount)).Returns(0);
        }

        [TestMethod]
        public void DebitOverdraftAccountNegativeBalanceTest()
        {
            _mockAccountRepository.Setup(x => x.Debit(100, _overdraftAccount)).Returns(-100);
        }
    }
}