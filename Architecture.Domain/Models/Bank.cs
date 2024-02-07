namespace Architecture.Domain.Models
{
    public class Bank : BaseModel
    {
        public List<Customer>? Customers { get; set; }
        public List<Account> Accounts { get; set; }

        public Bank() { }
        public void Withdrawal(string accountNumber, string clientName, int amount)
        {

        }
        public void Deposit(string accountNumber, string clientName, int amount)
        {

        }
        public void AccountOpening(string clientName)
        {

        }

        public void Consultation(string accountNumber)
        {

        }

        public float ConversionFromEuro(int euroAmount)
        {

        }

        public float ConversionToEuro(int amount)
        {

        }
    }
}
