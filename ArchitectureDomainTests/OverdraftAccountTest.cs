using Architecture.Domain.Models;

namespace ArchitectureDomainTests;

[TestClass]
public class OverdraftAccountTest
{
    private OverdraftAccount _overdraftAccount;

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