using Architecture.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Architecture.Impl.Repositories
{
    internal interface IAccountRepository
    {
        Account getAccountById(Guid accountId);
        Account getAccountByNumber(Guid accountNumber);
        List<Account> getAllAccounts();
        Account createAccount(Account account);
        Account updateAccount(Guid accountId, Account account);
        string deleteAccount(Guid accountId);
        int Debit(int amount, Account account);
        int Credit(int amount, Account account);
    }
}
