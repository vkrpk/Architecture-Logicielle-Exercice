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

        public virtual int Debit(int amount)
        {
            Balance += amount;
            return Balance;
        }
        public virtual int Credit(int amount)
        {
            Balance -= amount;
            return Balance;
        }
    }
}
