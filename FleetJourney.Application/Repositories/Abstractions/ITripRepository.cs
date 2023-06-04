using FleetJourney.Domain.Trips;

namespace FleetJourney.Application.Repositories.Abstractions;

public interface ITripRepository : IRepository<Trip, Guid>
{
	Task<IEnumerable<Trip>> GetAllByEmployeeIdAsync(Guid employeeId, CancellationToken cancellationToken);
	
	Task<Trip?> GetByCarPlateNumberAsync(string licensePlateNumber, CancellationToken cancellationToken);
}