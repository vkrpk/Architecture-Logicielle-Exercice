namespace Architecture.Domain.Models
{
    public abstract class Account : BaseModel
    {
        public int Balance;
        public Guid AccountNumber;
        public bool IsOverdraftAllowed { get; set; }
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }
        public Guid BankId { get; set; }
        public Bank Bank { get; set; }

        public Account(Customer customer) 
        {
            Customer = customer;
        }

    }
}
