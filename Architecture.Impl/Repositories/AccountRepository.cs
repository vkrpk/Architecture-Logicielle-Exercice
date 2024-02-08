using System;
using System.Collections.Generic;
using System.Linq;
using Architecture.Domain.Models;
using Architecture.Impl.Services;
using Microsoft.EntityFrameworkCore;

namespace Architecture.Impl.Repositories
{
    public class AccountRepository
    {
        private readonly AppDbContext _context;
        private IAccountService _accountService;

        public AccountRepository(AppDbContext context)
        {
            _context = context;
        }

        public Account getAccountById(Guid id)
        {
            return _context.Accounts.Find(id);
        }

        public void Add(Account account)
        {
            _context.Add(account);
            _context.SaveChanges();
        }

        public void Update(Account account)
        {
            _context.Accounts.Update(account);
            _context.SaveChanges();
        }

        public void Delete(Account account)
        {
            _context.Accounts.Remove(account);
            _context.SaveChanges();
        }



        public IEnumerable<Account> GetAll()
        {
            return _context.Accounts.ToList();
        }
    }
}
