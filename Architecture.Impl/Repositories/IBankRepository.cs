namespace Architecture.Impl.Repositories
{
    public interface IBankRepository
    {
        public Task Withdrawal(Guid accountNumber, string clientName, int amount);
        public Task Deposit(Guid accountNumber, string clientName, int amount);
        public Task AccountOpening(string clientName, bool isOverdraftAllowed);
        public Task<int> Consultation(Guid accountNumber);

        //TODO
        //public float ConversionFromEuro(int euroAmount);
        //public float ConversionToEuro(int amount);
    }
}
