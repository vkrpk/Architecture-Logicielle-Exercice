using Architecture.Domain.Models;
using System.Diagnostics;

namespace ArchitectureTests
{
    [TestClass]
    public class AccountTest
    {
        [TestMethod]
        public void CreditNoOverdraftAccountTest()
        {
            var testAccount = new NoOverdraftAccount();
            Assert.AreEqual(100, testAccount.Credit(100));
        }
        [TestMethod]
        public void CreditOverdraftAccountTest()
        {
            var testAccount = new OverdraftAccount();
            Assert.AreEqual(100, testAccount.Credit(100));
        }

        [DataRow(100, 0)]
        [TestMethod]
        public void DebitNoOverdraftAccountTest(int debit, int expected)
        {
            var testAccount = new NoOverdraftAccount();
            Assert.AreEqual(expected, testAccount.Debit(debit));
        }
        [DataRow(100, -100)]

        [TestMethod]
        public void DebitOverdraftAccountTest(int debit, int expected)
        {
            var testAccount = new OverdraftAccount();
            Assert.AreEqual(expected, testAccount.Debit(debit));
        }
    }
}