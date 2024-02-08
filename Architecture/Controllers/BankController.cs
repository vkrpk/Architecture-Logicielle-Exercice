namespace Architecture.Controllers;
using Architecture.Domain.Models;
using Microsoft.AspNetCore.Mvc;

public class BankController
{
    private readonly Bank _bank;

    public BankController(Bank bank)
    { 
        _bank = bank; 
    }


}