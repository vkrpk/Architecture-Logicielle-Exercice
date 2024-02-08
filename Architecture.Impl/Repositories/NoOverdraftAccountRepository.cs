using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Architecture.Impl.Repositories
{
    internal class NoOverdraftAccountRepository: AccountRepository
    {
        public override int Debit(int amount)
        {
            if (amount < base.Balance)
                return base.Debit(amount);
            else
                throw new OverdraftException("You don't have enough money");
        }
    }

    public class OverdraftException(string message) : Exception(message) { }
}
}
