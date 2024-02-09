using Architecture.Domain.Models;
using Architecture.Impl.EFDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Architecture.Impl.Repositories
{
    public class OverdraftAccountRepository(AppDbContext context) : AccountRepository(context), INoOverdraftAccountRepository
    {
    }
}

