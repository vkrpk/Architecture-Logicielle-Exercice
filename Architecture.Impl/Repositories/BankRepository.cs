using Architecture.Domain.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Architecture.Impl.Repositories
{
    internal class BankRepository: IBankRepository
    {
        public readonly IAccountRepository _accountRepository;
        public readonly ICustomerRepository _customerRepository;

        public BankRepository(IAccountRepository accountRepository, ICustomerRepository customerRepository)
        {
            _accountRepository = accountRepository;
            _customerRepository = customerRepository;
        }

        public void Withdrawal(Guid accountNumber, string clientName, int amount)
        {
            Account account = _accountRepository.getAccountByNumber(accountNumber);

            if (account.Customer.Name == clientName)
            {
                _accountRepository.Debit(amount, account);
            }
        }

        public void Deposit(Guid accountNumber, string clientName, int amount)
        {
            Account account = _accountRepository.getAccountByNumber(accountNumber);

            if (account.Customer.Name == clientName)
            {
                _accountRepository.Credit(amount, account);
            }
        }

        public void AccountOpening(string clientName)
        {
            Customer customer = _customerRepository.getCustomerByClientName(clientName);
            if (customer != null) 
            {
                
            }
            // get client by clientName
            // créer un nouveau compte avec le client
        }

        public int Consultation(Guid accountNumber)
        {
            Account account = _accountRepository.getAccountByNumber(accountNumber);
            return account.Balance;
        }

        public float ConversionFromEuro(int euroAmount)
        {
            // Quelle monnaie ? 
        }

        public float ConversionToEuro(int amount)
        {

        }
    }
}
