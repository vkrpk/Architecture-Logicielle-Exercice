using Architecture.Impl.EFDatabase;
using Architecture.Impl.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        // Add services to the container.
        builder.Services.AddRazorPages();

        #region Ajout DI Repositories
        builder.Services.AddScoped<IAccountRepository, AccountRepository>();
        builder.Services.AddScoped<INoOverdraftAccountRepository, NoOverdraftAccountRepository>();
        builder.Services.AddScoped<IBankRepository, BankRepository>();
        builder.Services.AddScoped<ICustomerRepository,CustomerRepository>();
        #endregion

        builder.Services.AddControllers();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Bank API",
                Version = "v1",
            });
        });

        var app = builder.Build();

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseRouting();


        app.UseAuthorization();

        app.MapRazorPages();

        app.Run();
    }
}