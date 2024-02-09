namespace Architecture.Impl.Repositories
{
    public interface IBankRepository
    {
        public void Withdrawal(Guid accountNumber, string clientName, int amount);
        public void Deposit(Guid accountNumber, string clientName, int amount);
        public void AccountOpening(string clientName, bool isOverdraftAllowed);
        public int Consultation(Guid accountNumber);

        //TODO
        //public float ConversionFromEuro(int euroAmount);
        //public float ConversionToEuro(int amount);
    }
}
