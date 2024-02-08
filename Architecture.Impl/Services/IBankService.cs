using System;
using Architecture.Domain.Models;
using Architecture.Impl.Repositories;
using Microsoft.Identity.Client;

namespace Architecture.Impl.Services
{
    public interface IBankService
    {

        Bank getBankById(Guid bankId);
        Bank createBank(Bank bank);
        Bank updateBank(Guid bankId, Bank bank);
        string deleteBank(Guid bankId);
    }
}
