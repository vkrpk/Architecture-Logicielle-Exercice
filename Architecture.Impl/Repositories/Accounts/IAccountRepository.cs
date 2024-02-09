using Architecture.Domain.Models;

namespace Architecture.Impl.Repositories
{
    public interface IAccountRepository
    {
        Task<Account> getAccountById(Guid accountId);
        Task<Account> getAccountByNumber(Guid accountNumber);
        Task<List<Account>> getAllAccounts();
        Task<List<Account>> getAccountsByCustomer(Customer customer);
        Task<Account> createAccount(Customer customer, bool isOverdraftAllowed);
        Task<Account> updateAccount(Account account);
        Task<string> deleteAccount(Guid accountId);
        Task<int> Debit(int amount, Account account);
        Task<int> Credit(int amount, Account account);
    }
}
