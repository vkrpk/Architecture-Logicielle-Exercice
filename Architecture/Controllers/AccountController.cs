using System;
using Microsoft.AspNetCore.Mvc;
using Architecture.Domain.Models;
using Architecture.Impl.Repositories;

namespace DefaultNamespace
{
    [ApiController]
    [Route("/api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepo;
        private readonly INoOverdraftAccountRepository _noOverdraftAccountRepo;

        public AccountController(IAccountRepository accountRepo, INoOverdraftAccountRepository noOverdraftAccountRepo)
        {
            _accountRepo = accountRepo;
            _noOverdraftAccountRepo = noOverdraftAccountRepo;
        }

        [HttpGet("{accountId}")]
        public IActionResult GetAccountById(Guid accountId)
        {
            Account account = _accountRepo.getAccountById(accountId);

            if (account == null)
                return NotFound();

            return Ok(account);
        }
        public IActionResult DebitAccount(int amount, Guid accountId)
        {
            Account account = _accountRepo.getAccountById(accountId);
            if (account.IsOverdraftAllowed)
                DebitAccount<OverdraftAccount>(amount, account);
            else
                DebitAccount<NoOverdraftAccount>(amount, account);
            return Ok(account);
        }
        private void DebitAccount<T>(int amount, Account account) where T : Account
        {
            var type = typeof(T);
            if (type == typeof(NoOverdraftAccount))
                _noOverdraftAccountRepo.Debit(amount, account); 
            else
                _accountRepo.Debit(amount, account);
        }
    }

}
