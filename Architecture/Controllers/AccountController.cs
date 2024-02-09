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
        private readonly ICustomerRepository _customerRepo;


        public AccountController(IAccountRepository accountRepo, INoOverdraftAccountRepository noOverdraftAccountRepo, ICustomerRepository customerRepo)
        {
            _accountRepo = accountRepo;
            _noOverdraftAccountRepo = noOverdraftAccountRepo;
            _customerRepo = customerRepo;
        }

        [HttpGet]
        public IActionResult GetAccounts()
        {
            List<Account> accounts = _accountRepo.getAllAccounts();

            if (accounts == null)
                return NotFound();

            return Ok(accounts);
        }

        [HttpGet("byId/{accountId}")]
        public IActionResult GetAccountById(Guid accountId)
        {
            Account account = _accountRepo.getAccountById(accountId);

            if (account == null)
                return NotFound();

            return Ok(account);
        }

        [HttpGet("byCustomer/{customerName}")]
        public IActionResult GetAccountsByCustomer(string customerName)
        {
            Customer customer = _customerRepo.getCustomerByClientName(customerName);
            if (customer == null)
                return NotFound();
            else
            {
                List<Account> accounts = _accountRepo.getAccountsByCustomer(customer);
                return Ok(accounts);
            }
        }

        [HttpPost("{amount}")]
        public IActionResult DebitAccount(int amount, [FromBody] Guid accountId)
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
