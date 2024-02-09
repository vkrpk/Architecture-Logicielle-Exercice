﻿using Architecture.Domain.Models;
using Architecture.Impl.EFDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Architecture.Impl.Repositories
{
    public class OverdraftAccountRepository(AppDbContext context) : AccountRepository(context), IOverdraftAccountRepository
    {
        public async override Task<int> Debit(int amount, Account account)
    {
            account.Balance -= amount;

            await updateAccount(account);

            return account.Balance;

        }

    }

    public class OverdraftException(string message) : Exception(message) { }
}

