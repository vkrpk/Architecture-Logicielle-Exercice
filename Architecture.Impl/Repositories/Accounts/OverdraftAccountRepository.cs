﻿using Architecture.Domain.Models;
using Architecture.Impl.EFDatabase;

namespace Architecture.Impl.Repositories
{
    public class OverdraftAccountRepository(AppDbContext context) : AccountRepository(context), IOverdraftAccountRepository
    {
        public override int Debit(int amount, Account account)
        {
            account.Balance -= amount;
            return account.Balance;

        }

    }

    public class OverdraftException(string message) : Exception(message) { }
}
