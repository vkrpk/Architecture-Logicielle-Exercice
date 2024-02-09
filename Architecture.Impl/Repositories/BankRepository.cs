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

        public async void AccountOpening(string clientName, bool isOverdraftAllowed)
        {
            Customer customer = await _customerRepository.getCustomerByClientName(clientName);
            if (customer != null) 
            {
                _accountRepository.createAccount(customer, isOverdraftAllowed);
            }
        }

        public int Consultation(Guid accountNumber)
        {
            Account account = _accountRepository.getAccountByNumber(accountNumber);
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
