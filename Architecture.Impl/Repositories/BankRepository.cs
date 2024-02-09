using Architecture.Domain.Models;

namespace Architecture.Impl.Repositories
{
    public class BankRepository : IBankRepository
    {
        public readonly IAccountRepository _accountRepository;
        public readonly ICustomerRepository _customerRepository;

        public BankRepository(IAccountRepository accountRepository, ICustomerRepository customerRepository)
        {
            _accountRepository = accountRepository;
            _customerRepository = customerRepository;
        }

        public async Task Withdrawal(Guid accountNumber, string clientName, int amount)
        {
            Account account = await _accountRepository.getAccountByNumber(accountNumber);

            if (account.Customer.Name == clientName)
            {
               await _accountRepository.Debit(amount, account);
            }
        }

        public async Task Deposit(Guid accountNumber, string clientName, int amount)
        {
            Account account = await _accountRepository.getAccountByNumber(accountNumber);

            if (account.Customer.Name == clientName)
            {
                await _accountRepository.Credit(amount, account);
            }
        }

        public async Task AccountOpening(string clientName, bool isOverdraftAllowed)
        {
            Customer customer = await _customerRepository.getCustomerByClientName(clientName);
            if (customer != null) 
            {
                await _accountRepository.createAccount(customer, isOverdraftAllowed);
            }
        }

        public async Task<int> Consultation(Guid accountNumber)
        {
            Account account = await _accountRepository.getAccountByNumber(accountNumber);
            return account.Balance;
        }

        //TODO
        //public int ConversionFromEuro(int euroAmount)
        //{
        //    // Quelle monnaie ? 
        //}

        //public int ConversionToEuro(int amount)
        //{

        //}
    }
}
