namespace Architecture.Domain.Models
{
    public class OverdraftAccount : Account
    {
        public new bool IsOverdraftAllowed = true;
    }
}
