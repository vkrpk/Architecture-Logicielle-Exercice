using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Architecture.Impl.Repositories
{
    internal interface IBankRepository
    {
        public void Withdrawal(Guid accountNumber, string clientName, int amount);
        public void Deposit(Guid accountNumber, string clientName, int amount);
        public void AccountOpening(string clientName);
        public int Consultation(Guid accountNumber);

        public float ConversionFromEuro(int euroAmount);
        public float ConversionToEuro(int amount);
    }
}
