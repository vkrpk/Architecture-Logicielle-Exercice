using Architecture.Domain.Models;
using Architecture.Impl.EFDatabase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;

namespace Architecture.Impl.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AppDbContext _context;

        public AccountRepository(AppDbContext context)
        {
            _context = context;
        }

        public Account getAccountById(Guid accountId)
        {
            try
            {
                return _context.Accounts.Include(a => a.Customer)
                    .First(a => a.Id == accountId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Impossible de trouver le compte avec l'ID {accountId}.", ex);
            }
        }

        public Account getAccountByNumber(Guid accountNumber)
        {
            try
            {
                return _context.Accounts.Include(a => a.Customer)
                    .First(a => a.AccountNumber == accountNumber);
            }
            catch (Exception ex)
            {
                throw new Exception($"Impossible de trouver le compte avec le numéro de compte {accountNumber}.", ex);
            }
        }

        public List<Account> getAllAccounts()
        {
            return _context.Accounts?.ToList();
        }

        public Account createAccount(Customer customer, bool isOverdraftAllowed)
        {
            Account newAccount;
            if (isOverdraftAllowed)
                newAccount = new OverdraftAccount { Customer = customer };
            else
                newAccount = new NoOverdraftAccount { Customer = customer };

            EntityEntry<Account> createdAccount = _context.Accounts.Add(newAccount);
            _context.SaveChanges();

            return createdAccount.Entity;
        }

        public async Task<Account> updateAccount(Account account)
        {
            var entity = await _context.Accounts.FindAsync(account.Id);
            _context.Entry(entity).CurrentValues.SetValues(account);
            EntityEntry<Account> updatedAccount = _context.Accounts.Update(account);
            await _context.SaveChangesAsync();

            return updatedAccount.Entity;
        }

        public string deleteAccount(Guid accountId)
        {
            Account account = getAccountById(accountId);
            _context.Accounts.Remove(account);
            _context.SaveChanges();

            return "Account " + accountId + " supprimé avec succès";
        }

        public virtual int Debit(int amount, Account account)
        {
            account.Balance -= amount;
            updateAccount(account);
            return account.Balance;
        }

        public int Credit(int amount, Account account)
        {
            account.Balance += amount;
            updateAccount(account);
            return account.Balance;
        }

        public List<Account> getAccountsByCustomer(Customer customer)
        {
            return _context.Accounts.Where(a => a.CustomerId == customer.Id).ToList();
        }
    }
}
