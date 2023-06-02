namespace FleetJourney.IdentityApi.Persistence.Abstractions;

internal interface IIdentityDbContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}