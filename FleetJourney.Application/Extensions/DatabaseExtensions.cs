using FleetJourney.Application.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MySqlConnector;

namespace FleetJourney.Application.Extensions;

public static class DatabaseExtensions
{
    private static IServiceCollection AddMySqlDbContextOptions<TDbContext>(this IServiceCollection services,
        string connectionString)
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

    public static IServiceCollection AddApplicationDbContext<TDbInterface, TDbContext>(this IServiceCollection services,
        string connectionString)
        where TDbContext : DbContext, TDbInterface
        where TDbInterface : class
    {
        services.AddMySql<TDbContext>(connectionString, ServerVersion.AutoDetect(connectionString));
        services.AddMySqlDbContextOptions<TDbContext>(connectionString);

        services.AddApplicationService<TDbInterface>(services.Single(x =>
        {
            return x.ImplementationType == typeof(TDbContext);
        }).Lifetime);
        return services;
    }
}