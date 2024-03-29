﻿using Architecture.Domain.Models;
using Architecture.Impl.EFDatabase;
using Microsoft.Identity.Client;


namespace Architecture.Impl.Repositories
{
    public class NoOverdraftAccountRepository(AppDbContext context) : AccountRepository(context), INoOverdraftAccountRepository
    {
        public override int Debit(int amount, Account account)
        {
            if (amount <= account.Balance)
            {
                account.Balance -= amount;

                updateAccount(account);

                return account.Balance;
            }
            else
                throw new NoOverdraftException("You don't have enough money");
        }
    }

    public class NoOverdraftException(string message) : Exception(message) { }
}

