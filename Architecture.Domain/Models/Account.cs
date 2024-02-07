namespace Architecture.Domain.Models
{
    public abstract class Account : BaseModel
    {
        protected int Balance;
        protected string AccountNumber;
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }
        public Guid BankId { get; set; }
        public Bank Bank { get; set; }

    }
}
