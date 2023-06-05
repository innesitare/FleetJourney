using FleetJourney.Domain.Trips;

namespace FleetJourney.Application.Services.Abstractions;

public interface ITripService
{
    Task<IEnumerable<Trip>> GetAllAsync(CancellationToken cancellationToken);

    Task<IEnumerable<Trip>> GetAllByEmployeeIdAsync(Guid employeeId, CancellationToken cancellationToken);

    Task<Trip?> GetAsync(Guid tripId, CancellationToken cancellationToken);

    Task<Trip?> GetTripByCarIdAsync(Guid carId, CancellationToken cancellationToken);

    Task<bool> CreateAsync(Trip trip, CancellationToken cancellationToken);

    Task<Trip?> UpdateAsync(Trip trip, CancellationToken cancellationToken);

    Task<bool> DeleteAsync(Guid tripId, CancellationToken cancellationToken);
}