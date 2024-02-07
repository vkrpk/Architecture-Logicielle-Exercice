namespace Architecture.Domain.Models
{
    public class Customer : BaseModel
    {
        public string ClientNumber { get; set; }
        public string Address { get; set; }
        public string Name { get; set; }
        public Guid BankId { get; set; }
        public Bank Bank { get; set; }
        public List<Account> Accounts { get; set; }
    }
}
