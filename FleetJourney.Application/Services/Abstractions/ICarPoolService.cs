using FleetJourney.Domain.CarPool;

namespace FleetJourney.Application.Services.Abstractions;

public interface ICarPoolService
{
    Task<IEnumerable<Car>> GetAllAsync(CancellationToken cancellationToken);

    Task<Car?> GetAsync(Guid id, CancellationToken cancellationToken);

    Task<bool> CreateAsync(Car car, CancellationToken cancellationToken);
    
    Task<Car?> UpdateAsync(Car car, CancellationToken cancellationToken);
    
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
}