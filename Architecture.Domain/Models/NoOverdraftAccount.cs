namespace Architecture.Domain.Models
{
    public class NoOverdraftAccount : Account
    {
        public override int Debit(int amount)
        {
            if (amount <= base.Balance)
                return base.Balance - amount;
            else
                throw new OverdraftException("You don't have enough money");
        }
    }

    public class OverdraftException (string message) : Exception(message) { } 
}
