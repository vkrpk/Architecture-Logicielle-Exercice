using Architecture.Impl.EFDatabase;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var config = builder.Configuration;

        builder.Services.AddDbContext<AppDbContext>(options
            => options.UseSqlServer(config.GetConnectionString("ConnectionString")));

        // Add services to the container.
        builder.Services.AddRazorPages();

        var app = builder.Build();

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapRazorPages();

        app.Run();
    }
}