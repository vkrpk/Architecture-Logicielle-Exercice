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

        public async Task<Account> getAccountById(Guid accountId)
        {
            try
            {
                return await _context.Accounts.Include(a => a.Customer)
                    .FirstAsync(a => a.Id == accountId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Impossible de trouver le compte avec l'ID {accountId}.", ex);
            }
        }

        public async Task<Account> getAccountByNumber(Guid accountNumber) 
        {
            try
            {
                return await _context.Accounts.Include(a => a.Customer)
                    .FirstAsync(a => a.AccountNumber == accountNumber);
            }
            catch (Exception ex)
            {
                throw new Exception($"Impossible de trouver le compte avec le numéro de compte {accountNumber}.", ex);
            }
        }

        public async Task<List<Account>> getAllAccounts()
        {
            return await _context.Accounts.ToListAsync();
        }
        public List<Account> getAccountsByCustomer(Customer customer)
        {
            return _context.Accounts.Where(a => a.CustomerId == customer.Id).ToList();
        }

        public async Task<Account> createAccount(Customer customer, bool isOverdraftAllowed)
        {
            Account newAccount;
            if(isOverdraftAllowed)
                newAccount = new OverdraftAccount();
            else 
                newAccount = new NoOverdraftAccount();

            EntityEntry<Account> createdAccount = _context.Accounts.Add(newAccount);
            await _context.SaveChangesAsync();

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

        public async Task<string> deleteAccount(Guid accountId)
        {
            Account account = await getAccountById(accountId);
            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();

            return "Account " + accountId + " supprimé avec succès";
        }

        public async virtual Task<int> Debit(int amount, Account account)
        {
            account.Balance -= amount;
            _context.Accounts.Update(account);

            await _context.SaveChangesAsync();

            return account.Balance;
        }

        public async Task<int> Credit(int amount, Account account)
        {
            account.Balance += amount;

            _context.Accounts.Update(account);
            await _context.SaveChangesAsync();

            return account.Balance;
        }

        public async Task<List<Account>> getAccountsByCustomer(Customer customer)
        {
            return await _context.Accounts.Where(a => a.CustomerId == customer.Id).ToListAsync();
        }
    }
}
