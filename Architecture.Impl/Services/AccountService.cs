using System;
using Architecture.Domain.Models;
using Architecture.Impl.Repositories;
using Microsoft.Identity.Client;

namespace Architecture.Impl.Services
{
    public interface IAccountService
    {

        Account getAccountById(Guid accountId);
        List<Account> getAllAccounts();
        Account createAccount(Account account);
        Account updateAccount(Guid accountId, Account account);
        string deleteAccount(Guid accountId);
    }
}
