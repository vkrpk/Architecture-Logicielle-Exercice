using System;
using Microsoft.AspNetCore.Mvc;
using Architecture.Domain.Models;
using Architecture.Impl.Repositories;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace Architecturee.Controllers
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

        [HttpGet("{accountId}")]
        public IActionResult GetAccountById(Guid accountId)
        {
            Account account = _accountRepo.getAccountById(accountId);

            if (account == null)
                return NotFound();

            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            string json = JsonSerializer.Serialize(account, options);

            return Ok(json);
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

                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = true
                };

                string json = JsonSerializer.Serialize(accounts, options);
                return Ok(json);
            }
        }

        [HttpPut("Debit/{amount}")]
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
