using DotNet.Testcontainers.Builders;
using Testcontainers.MySql;
using Xunit;

namespace FleetJourney.Tests.Models;

public sealed class MySqlContainerFixture : IAsyncLifetime
{
    private readonly int _port = Random.Shared.Next(33070, 33100);
    public readonly MySqlContainer MySqlContainer;
    
    public MySqlContainerFixture()
    {
        MySqlContainer = new MySqlBuilder()
            .WithImage("mysql:8.0.32")
            .WithUsername("admin")
            .WithPassword("admin")
            .WithDatabase("testStore")
            .WithEnvironment("MYSQL_RANDOM_ROOT_PASSWORD", "YES")
            .WithCommand("--default-authentication-plugin=caching_sha2_password")
            .WithPortBinding(_port, 3306)
            .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(3306))
            .WithExposedPort(3306)
            .Build();
    }
    
    public Task InitializeAsync()
    {
        return MySqlContainer.StartAsync();
    }

    public Task DisposeAsync()
    {
        return MySqlContainer.StopAsync();
    }
}