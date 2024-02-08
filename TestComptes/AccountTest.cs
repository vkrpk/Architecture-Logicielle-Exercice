using Architecture.Domain.Models;
using System.Diagnostics;

namespace ArchitectureTests
{
    [TestClass]
    public class AccountTest
    {
        private OverdraftAccount overdraftAccount;
        private NoOverdraftAccount noOverdraftAccount;

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
            Assert.ThrowsException<OverdraftException>(()=> noOverdraftAccount.Debit(2000));
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