namespace Architecture.Domain.Models
{
    public abstract class Account : BaseModel
    {
        protected int Balance;
        protected string AccountNumber;

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
