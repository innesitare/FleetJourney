using FleetJourney.Domain.CarPool;

namespace FleetJourney.Core.Services.Abstractions;

public interface ICarPoolService
{
    Task<IEnumerable<Car>> GetAllAsync(CancellationToken cancellationToken);

    Task<Car?> GetAsync(string licensePlateNumber, CancellationToken cancellationToken);

    Task<bool> CreateAsync(Car car, CancellationToken cancellationToken);
    
    Task<Car?> UpdateAsync(Car car, CancellationToken cancellationToken);
    
    Task<bool> DeleteAsync(string licensePlateNumber, CancellationToken cancellationToken);
}