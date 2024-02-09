namespace Architecture.Domain.Models
{
    public class NoOverdraftAccount : Account
    {
        public new bool IsOverdraftAllowed = false;
    }
}
