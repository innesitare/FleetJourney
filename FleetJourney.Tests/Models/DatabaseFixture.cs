using Microsoft.EntityFrameworkCore;

namespace FleetJourney.Tests.Models;

public sealed class DatabaseFixture<TEntity> : IDisposable 
    where TEntity : DbContext
{
    public readonly TEntity DbContext;

    public DatabaseFixture()
    {
        var options = new DbContextOptionsBuilder<TEntity>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .EnableSensitiveDataLogging()
            .Options;

        DbContext = (Activator.CreateInstance(typeof(TEntity), options) as TEntity)!;
    }

    public void Dispose()
    {
        DbContext.Database.EnsureDeleted();
        DbContext.Dispose();
    }
}