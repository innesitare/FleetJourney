using FleetJourney.Application.Extensions;
using FleetJourney.Application.Tests.Models;
using FleetJourney.Infrastructure.Persistence;
using FleetJourney.Infrastructure.Persistence.Abstractions;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.MySql;
using Xunit;

namespace FleetJourney.Application.Tests.Extensions;

public sealed class DatabaseExtensionsTests : IClassFixture<MySqlContainerFixture>
{
    private readonly MySqlContainer _mySqlContainer;

    public DatabaseExtensionsTests(MySqlContainerFixture mySqlContainerFixture)
    {
        _mySqlContainer = mySqlContainerFixture.MySqlContainer;
    }

    [Fact]
    public Task AddDatabase_Should_RegisterDbContextInDIContainer()
    {
        // Arrange
        var services = new ServiceCollection();
        var connectionString = $"Server={_mySqlContainer.Hostname};Port={_mySqlContainer.GetMappedPublicPort(3306)};Database=testStore;User ID=admin;Password=admin;SSL Mode=Required;";

        // Act
        services.AddApplicationDbContext<IApplicationDbContext, ApplicationDbContext>(connectionString);
        var serviceProvider = services.BuildServiceProvider();
        var dbContext = serviceProvider.GetService<ApplicationDbContext>();
        
        // Assert
        dbContext.Should().NotBeNull();
        dbContext!.Database.Should().NotBeNull();
        dbContext.Database.GetConnectionString().Should().NotBeNull();
        
        return Task.CompletedTask;
    }
}