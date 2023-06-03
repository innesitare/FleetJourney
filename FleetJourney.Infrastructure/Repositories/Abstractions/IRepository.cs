namespace FleetJourney.Infrastructure.Repositories.Abstractions;

public interface IRepository<TEntity>
    where TEntity : class
{
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken);
    
    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    
    Task<bool> CreateAsync(TEntity entity, CancellationToken cancellationToken);
    
    Task<TEntity?> UpdateAsync(TEntity entity, CancellationToken cancellationToken);
    
    Task<bool> DeleteByIdAsync(Guid id, CancellationToken cancellationToken);
}