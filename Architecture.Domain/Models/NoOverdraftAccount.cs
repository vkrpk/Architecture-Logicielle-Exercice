namespace Architecture.Domain.Models
{
    public class NoOverdraftAccount : Account
    {
        public new bool IsOverdraftAllowed = false;
    }

    public class OverdraftException (string message) : Exception(message) { } 
}
