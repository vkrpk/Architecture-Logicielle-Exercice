using Architecture.Domain.Models;
using Architecture.Impl.EFDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Architecture.Impl.Repositories
{
    public class NoOverdraftAccountRepository(AppDbContext context) : AccountRepository(context), INoOverdraftAccountRepository
    {
        public override int Debit(int amount, Account account)
        {
            if (amount < account.Balance)
            {
                account.Balance -= amount;
                return account.Balance;

            }
            else
                throw new OverdraftException("You don't have enough money");
        }
    }

    public class OverdraftException(string message) : Exception(message) { }
}

