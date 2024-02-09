using Architecture.Domain.Models;
using Architecture.Impl.EFDatabase;
<<<<<<< HEAD
using Microsoft.EntityFrameworkCore;
=======
using Microsoft.Identity.Client;
>>>>>>> 3de328d1a73ee60268b4d224cd0362781e222d88

namespace Architecture.Impl.Repositories
{
    public class NoOverdraftAccountRepository(AppDbContext context) : AccountRepository(context), INoOverdraftAccountRepository
    {
        public async override Task<int> Debit(int amount, Account account)
        {
            if (amount <= account.Balance)
            {
                account.Balance -= amount;

                await updateAccount(account);

                return account.Balance;
            }
            else
                throw new NoOverdraftException("You don't have enough money");
        }
    }

    public class NoOverdraftException(string message) : Exception(message) { }
}

