namespace FleetJourney.Infrastructure.Repositories.Abstractions;

public interface IRepository<TEntity, in TKey>
    where TEntity : class
{
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken);
    
    Task<TEntity?> GetAsync(TKey key, CancellationToken cancellationToken);
    
    Task<bool> CreateAsync(TEntity entity, CancellationToken cancellationToken);
    
    Task<TEntity?> UpdateAsync(TEntity entity, CancellationToken cancellationToken);
    
    Task<bool> DeleteAsync(TKey key, CancellationToken cancellationToken);
}