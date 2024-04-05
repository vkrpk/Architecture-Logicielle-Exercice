using Architecture.Impl.EFDatabase;
using Architecture.Impl.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;

internal class Program
{
    private static string connectionString = string.Empty;
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddRazorPages();

        #region Ajout DI Repositories
        builder.Services.AddScoped<IAccountRepository, AccountRepository>();
        builder.Services.AddScoped<INoOverdraftAccountRepository, NoOverdraftAccountRepository>();
        builder.Services.AddScoped<IBankRepository, BankRepository>();
        builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
        #endregion

        var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
        var dbName = Environment.GetEnvironmentVariable("DB_NAME");
        var dbPassword = Environment.GetEnvironmentVariable("DB_SA_PASSWORD");
        var dbPort = Environment.GetEnvironmentVariable("DB_PORT");

        if(dbPort == null || dbHost == null || dbName == null || dbPassword == null)
        {
            connectionString = builder.Configuration.GetConnectionString("Database") ?? "";
        }
        else
        {
            connectionString = $"Server={dbHost},{dbPort};Database={dbName};User Id=sa;Password={dbPassword};Encrypt=False;MultipleActiveResultSets=True;";
        }

        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString));


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

        await SeedInitData(app);

        app.Run();

        async Task SeedInitData(WebApplication app)
        {
            try
            {
                using (var scope = app.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;
                    var ctx = services.GetRequiredService<AppDbContext>();
                    if (ctx.Database.CanConnect())
                    {
                        await ctx.Database.MigrateAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while seeding the database: {ex.Message}");
            }
        }
    }
}