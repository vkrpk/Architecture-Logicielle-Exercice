namespace Architecture.Domain.Models
{
    public class Bank : BaseModel
    {
        public List<Customer>? Customers { get; set; }
        public List<Account> Accounts { get; set; }

        public Bank() { }
        public void Withdrawal(string accountNumber, string clientName, int amount)
        {
            // besoin d'un get account by account number 
            // check si le client correspond ? 
            // Retirer l'argent du solde
        }
        public void Deposit(string accountNumber, string clientName, int amount)
        {
            // besoin d'un get account by account number 
            // check si le client correspond ? 
            // Ajouter l'argent sur le solde
        }
        public void AccountOpening(string clientName)
        {
            // get client by clientName
            // créer un nouveau compte avec le client
        }

        public void Consultation(string accountNumber)
        {
            // Get account by accountNumber
            // Get solde
        }

        public float ConversionFromEuro(int euroAmount)
        {
            // Quelle monnaie ? 
        }

        public float ConversionToEuro(int amount)
        {

        }
    }
}
