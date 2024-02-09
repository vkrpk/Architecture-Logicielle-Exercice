using Architecture.Domain.Models;
using Architecture.Impl.EFDatabase;
using Microsoft.EntityFrameworkCore;

namespace Architecture.Impl.Repositories
{
    public class NoOverdraftAccountRepository(AppDbContext context) : AccountRepository(context), INoOverdraftAccountRepository
    {
        public async override Task<int> Debit(int amount, Account account)
        {
            if (amount < account.Balance)
            {
                account.Balance -= amount;
                _context.Accounts.Update(account);

                await _context.SaveChangesAsync();

                return account.Balance;
            }
            else
                throw new NoOverdraftException("You don't have enough money");
        }
    }

    public class NoOverdraftException(string message) : Exception(message) { }
}

