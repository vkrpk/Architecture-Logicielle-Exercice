namespace Architecture.Domain.Models
{
    public class OverdraftAccount(Customer customer) : Account(customer)
    {
        public new bool IsOverdraftAllowed = true;
    }
}
