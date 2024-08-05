using circles.infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace circles.infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
    IConfiguration config)
    {

        string? connectionString = config.GetConnectionString("CirclesConnectionString");

        services.AddDbContextFactory<CirclesDbContext>(options =>
          options.UseNpgsql(connectionString));

        return services;
    }
}