using Architecture.Domain.Models;
using Architecture.Impl.EFDatabase;
using Architecture.Impl.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

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

        // Add services to the container
        var jwtSettings = builder.Configuration.GetSection("Jwt");


        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(o =>
        {
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey
                (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SigningKey"])),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true
            };
            o.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = context =>
                {
                    Console.WriteLine("Authentication failed: " + context.Exception.Message);
                    return Task.CompletedTask;
                },
                OnTokenValidated = context =>
                {
                    Console.WriteLine("Token validated: " + context.SecurityToken);
                    return Task.CompletedTask;
                },
                OnChallenge = context =>
                {
                    Console.WriteLine("OnChallenge error: " + context.Error + ", " + context.ErrorDescription);
                    return Task.CompletedTask;
                }
            };
        });

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

        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<AppDbContext>();
                context.Database.Migrate();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while seeding the database: {ex.Message}");
            }
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}"
        );
            //.RequireAuthorization();

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
                    await ctx.Database.MigrateAsync();

                    if (!ctx.Banks.Any())
                    {
                        List<Bank> banks = new List<Bank>
                        {
                            new Bank(),
                            new Bank(),
                            new Bank()
                        };

                        ctx.Banks.AddRange(banks);
                        await ctx.SaveChangesAsync();
                    }
                    if (!ctx.Customers.Any())
                    {
                        List<Customer> customers = new List<Customer>
                        {
                            new Customer()
                            {
                                ClientNumber = "clientNumber100000",
                                Address = "Test Address 1",
                                Name = "First Client",
                                BankId = ctx.Banks.First().Id
                            },
                            new Customer()
                            {
                                ClientNumber = "clientNumber200000",
                                Address = "Test Address 2",
                                Name = "Second Client",
                                BankId = ctx.Banks.First().Id
                            },
                            new Customer()
                            {
                                ClientNumber = "clientNumber300000",
                                Address = "Test Address 3",
                                Name = "Third Client",
                                BankId = ctx.Banks.First().Id
                            },
                            new Customer()
                            {
                                ClientNumber = "clientNumber400000",
                                Address = "Test Address 4",
                                Name = "Fourth Client",
                                BankId = ctx.Banks.First().Id
                            },
                            new Customer()
                            {
                                ClientNumber = "clientNumber500000",
                                Address = "Test Address 5",
                                Name = "test@test.fr",
                                BankId = ctx.Banks.First().Id
                            }
                        };
                        ctx.Customers.AddRange(customers);
                        await ctx.SaveChangesAsync();
                    }
                    if (!ctx.Accounts.Any())
                    {
                        List<Account> accounts = new List<Account>
                        {
                            new NoOverdraftAccount()
                            {
                                Balance = 123456,
                                AccountNumber = new Guid(),
                                CustomerId = ctx.Customers.First().Id,
                                BankId = ctx.Banks.First().Id

                            },
                            new NoOverdraftAccount()
                            {
                                Balance = 123456,
                                AccountNumber = new Guid(),
                                CustomerId = ctx.Customers.First().Id,
                                BankId = ctx.Banks.First().Id
                            },
                            new OverdraftAccount()
                            {
                                Balance = 123456,
                                AccountNumber = new Guid(),
                                CustomerId = ctx.Customers.First().Id,
                                BankId = ctx.Banks.First().Id
                            },
                            new OverdraftAccount()
                            {
                                Balance = 123456,
                                AccountNumber = new Guid(),
                                CustomerId = ctx.Customers.First().Id,
                                BankId = ctx.Banks.First().Id
                            }
                        };
                        ctx.Accounts.AddRange(accounts);
                        await ctx.SaveChangesAsync();
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