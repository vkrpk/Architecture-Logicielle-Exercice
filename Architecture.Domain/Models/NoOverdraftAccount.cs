namespace Architecture.Domain.Models
{
    public class NoOverdraftAccount(Customer customer) : Account(customer)
    {
        public new bool IsOverdraftAllowed = false;
    }

    public class OverdraftException (string message) : Exception(message) { } 
}
