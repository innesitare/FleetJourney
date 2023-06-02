using FleetJourney.Core.Extensions;
using FleetJourney.IdentityApi.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MySqlConnector;

namespace FleetJourney.IdentityApi.Extensions;

public static class DbContextExtensions
{
    private static IServiceCollection AddMySqlDbContextOptions<TDbContext>(this IServiceCollection services, string connectionString)
        where TDbContext : DbContext
    {
        services.AddTransient<IOptions<MySqlDbContextSettings<TDbContext>>>(_ =>
        {
            var mySqlbuilder = new MySqlConnectionStringBuilder(connectionString);
            return Options.Create(new MySqlDbContextSettings<TDbContext>
            {
                ConnectionString = connectionString,
                Database = mySqlbuilder.Database,
                Host = mySqlbuilder.Server,
                Port = mySqlbuilder.Port,
                ServerVersion = ServerVersion.AutoDetect(mySqlbuilder.ConnectionString)
            });
        });

        return services;
    }
    
    public static IServiceCollection AddApplicationDbContext<TDbInterface, TDbContext>(this IServiceCollection services, string connectionString)
        where TDbContext : DbContext, TDbInterface
        where TDbInterface : class
    {
        services.AddMySql<TDbContext>(connectionString, ServerVersion.AutoDetect(connectionString));
        services.AddMySqlDbContextOptions<TDbContext>(connectionString);

        services.AddApplicationService<TDbInterface>(services.Single(x => x.ImplementationType == typeof(TDbContext)).Lifetime);
        return services;
    }
}