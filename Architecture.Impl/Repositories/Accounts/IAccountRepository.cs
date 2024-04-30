using Architecture.Domain.Models;

namespace Architecture.Impl.Repositories
{
    public interface IAccountRepository
    {
        Account getAccountById(Guid accountId);
        Account getAccountByNumber(Guid accountNumber);
        List<Account> getAllAccounts();
        List<Account> getAccountsByCustomer(Customer customer);
        Account createAccount(Account account);
        Task<Account> updateAccount(Account account);
        string deleteAccount(Guid accountId);
        int Debit(int amount, Account account);
        int Credit(int amount, Account account);
    }
}
