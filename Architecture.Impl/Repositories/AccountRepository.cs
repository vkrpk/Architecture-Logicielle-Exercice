using System;
using System.Collections.Generic;
using System.Linq;
using Architecture.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Architecture.Impl.EFDatabase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Architecture.Impl.Repositories
{
    public class AccountRepository: IAccountRepository
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
                    .FirstOrDefault(a => a.Id == accountId);
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
                    .FirstOrDefault(a => a.AccountNumber == accountNumber);
            }
            catch (Exception ex)
            {
                throw new Exception($"Impossible de trouver le compte avec le numéro de compte {accountNumber}.", ex);
            }
        }

        public List<Account> getAllAccounts()
        {
            return _context.Accounts.ToList();

        }

        public Account createAccount(Account account)
        {
            EntityEntry<Account> createdAccount = _context.Accounts.Add(account);
            _context.SaveChanges();

            return createdAccount.Entity;
        }

        public Account updateAccount(Guid accountId, Account account)
        {
            EntityEntry<Account> updatedAccount = _context.Accounts.Update(account);
            _context.SaveChanges();

            return updatedAccount.Entity;
        }

        public string deleteAccount(Guid accountId)
        {
            Account account = getAccountById(accountId);
            _context.Accounts.Remove(account);
            _context.SaveChanges();

            return "Account " + accountId + " supprimé avec succès";
        }

        public int Debit(int amount, Account account)
        {
            account.Balance -= amount;
            return account.Balance;
        }

        public int Credit(int amount, Account account)
        {
            account.Balance += amount;
            return account.Balance;
        }

    }
}
