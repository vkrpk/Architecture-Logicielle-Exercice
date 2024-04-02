using Architecture.Impl.EFDatabase;
using Architecture.Impl.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddRazorPages();

        var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
        var dbName = Environment.GetEnvironmentVariable("DB_NAME");
        var dbPassword = Environment.GetEnvironmentVariable("DB_SA_PASSWORD");
        var connectionString = $"Data Source=127.0.0.1,8002;Initial Catalog={dbName};User ID=sa;Password={dbPassword};Encrypt=False;";
        Console.WriteLine($"Chaîne de connexion: {connectionString}");
        builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

        #region Ajout DI Repositories
        builder.Services.AddScoped<IAccountRepository, AccountRepository>();
        builder.Services.AddScoped<INoOverdraftAccountRepository, NoOverdraftAccountRepository>();
        builder.Services.AddScoped<IBankRepository, BankRepository>();
        builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
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