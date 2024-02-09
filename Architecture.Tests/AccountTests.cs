using Architecture.Domain.Models;
using Architecture.Impl.Repositories;

namespace Architecture.Tests
{
    // Victor
    [TestClass]
    public class AccountTests
    {
        private OverdraftAccount _overdraftAccount;
        private NoOverdraftAccount noOverdraftAccount;


        [TestInitialize]
        public void Init()
        {
            _overdraftAccount = new OverdraftAccount();
        }

        [TestMethod]
        public void CreditOverdraftAccountNominalTest()
        {
            Assert.AreEqual(100, _overdraftAccount.Credit(100));
        }

        [TestMethod]
        public void DebitOverdraftAccountNominalTest()
        {
            _overdraftAccount.Credit(100);
            Assert.AreEqual(0, _overdraftAccount.Debit(100));
        }

        [TestMethod]
        public void DebitOverdraftAccountNegativeBalanceTest()
        {
            Assert.AreEqual(-100, _overdraftAccount.Debit(100));
        }
    }

    //Hervé
    [TestClass]
    public class AccountTest
    {
        private OverdraftAccount overdraftAccount;
        private NoOverdraftAccount noOverdraftAccount;
        private Mock mockService = new Mock<IAccountRepository>();

        [TestInitialize]
        public void Init()
        {
            overdraftAccount = new OverdraftAccount();
            noOverdraftAccount = new NoOverdraftAccount();

        }

        [TestMethod]
        public void CreditNoOverdraftAccountTest()
        {
            Assert.AreEqual(100, overdraftAccount.Credit(100));
        }
        [TestMethod]
        public void CreditOverdraftAccountTest()
        {
            Assert.AreEqual(100, overdraftAccount.Credit(100));
        }

        [TestMethod]
        public void DebitNoOverdraftAccountTest()
        {
            Assert.ThrowsException<OverdraftException>(() => noOverdraftAccount.Debit(2000));
        }
        [DataRow(100, -100)]

        [TestMethod]
        public void DebitOverdraftAccountTest(int debit, int expected)
        {
            Assert.AreEqual(expected, overdraftAccount.Debit(debit));
        }

        [TestMethod]
        public void DebitOverdraftAccountNominalTest()
        {
            Assert.AreEqual(100, noOverdraftAccount.Credit(100));
        }
    }
}