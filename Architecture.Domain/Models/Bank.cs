namespace Architecture.Domain.Models
{
    public class Bank : BaseModel
    {
        public List<Customer>? Customers { get; set; }
        public List<Account> Accounts { get; set; }

    }
}
