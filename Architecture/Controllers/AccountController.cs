using System;
using Microsoft.AspNetCore.Mvc;
using Architecture.Domain.Models;

namespace DefaultNamespace
{
    [ApiController]
    [Route("/api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly Account _noOverdraftAccount;
        private readonly Account _overdraftAccount;

        public AccountController()
        {
            _noOverdraftAccount = new NoOverdraftAccount();
            _overdraftAccount = new OverdraftAccount();
        }

        [HttpGet("{accountId}")]
        public IActionResult GetAccountById(Guid accountId)
        {
            Account account = GetAccountFromDatabase(accountId);

            if (account == null)
            {
                return NotFound();
            }

            return Ok(account);
        }

        private Account GetAccountFromDatabase(Guid accountId)
        {
            Account account = FindAccountInDatabase<NoOverdraftAccount>(accountId);
            if (account == null)
            {
                account = FindAccountInDatabase<OverdraftAccount>(accountId);
            }
            return account;
        }

        public IActionResult DebitAccount(Guid accountId)
        {
            Account account = GetAccountById(accountId);
            if (account == null)
            {
                return NotFound();
            }

            return Ok(account);
        }

        private Account FindAccountInDatabase<T>(Guid accountId) where T : Account
        {
            return null;
        }
    }

}
