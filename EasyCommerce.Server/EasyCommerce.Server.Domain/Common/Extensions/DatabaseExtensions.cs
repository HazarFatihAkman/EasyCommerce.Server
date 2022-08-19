using EasyCommerce.Server.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EasyCommerce;

public static class DatabaseExtensions
{
    public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(nameof(ApplicationDbContext));
        var migrationAssembly = typeof(ApplicationDbContext).Assembly.GetName().Name;
        services
            .AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>())
            .AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString, options => options.MigrationsAssembly(migrationAssembly))
        );
    }
}
