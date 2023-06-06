using FleetJourney.Application.Extensions;
using FleetJourney.Application.Repositories.Abstractions;
using FleetJourney.Domain.Trips;
using FleetJourney.Infrastructure.Persistence.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace FleetJourney.Application.Repositories;

internal sealed class TripRepository : ITripRepository
{
    private readonly IApplicationDbContext _applicationDbContext;

    public TripRepository(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<IEnumerable<Trip>> GetAllAsync(CancellationToken cancellationToken)
    {
        bool isEmpty = await _applicationDbContext.Trips.AnyAsync(cancellationToken);
        if (!isEmpty)
        {
            return Enumerable.Empty<Trip>();
        }

        return _applicationDbContext.Trips;
    }

    public async Task<IEnumerable<Trip>> GetAllByEmployeeIdAsync(Guid employeeId, CancellationToken cancellationToken)
    {
        bool hasTrips = await _applicationDbContext.Trips.AnyAsync(t => t.EmployeeId == employeeId, cancellationToken);
        if (!hasTrips)
        {
            return Enumerable.Empty<Trip>();
        }

        var trips = await _applicationDbContext.Trips
            .Where(t => t.EmployeeId == employeeId)
            .ToListAsync(cancellationToken);

        return trips;
    }
    
    public async Task<Trip?> GetAsync(Guid tripId, CancellationToken cancellationToken)
    {
        var trip = await _applicationDbContext.Trips.FindAsync(new object?[] {tripId}, cancellationToken);

        return trip;
    }

    public async Task<Trip?> GetByCarIdAsync(Guid carId, CancellationToken cancellationToken)
    {
        var trip = await _applicationDbContext.Trips
            .FirstOrDefaultAsync(t => t.CarId == carId, cancellationToken);

        return trip;
    }

    public async Task<bool> CreateAsync(Trip trip, CancellationToken cancellationToken)
    {
        bool carExists = await _applicationDbContext.Cars.ExistsAsync(c => c.Id, trip.CarId, cancellationToken);
        bool employeeExists = await _applicationDbContext.Employees
            .ExistsAsync(e => e.Id, trip.EmployeeId, cancellationToken);

        if (!carExists || !employeeExists)
        {
            return false;
        }
        
        await _applicationDbContext.Trips.AddAsync(trip, cancellationToken);
        int result = await _applicationDbContext.SaveChangesAsync(cancellationToken);

        return result > 0;
    }

    public async Task<Trip?> UpdateAsync(Trip trip, CancellationToken cancellationToken)
    {
        bool carExists = await _applicationDbContext.Cars.ExistsAsync(c => c.Id, trip.CarId, cancellationToken);
        bool employeeExists = await _applicationDbContext.Employees
            .ExistsAsync(e => e.Id, trip.EmployeeId, cancellationToken);
        
        if (!carExists || !employeeExists)
        {
            return null;
        }
        
        _applicationDbContext.Trips.Update(trip);
        await _applicationDbContext.SaveChangesAsync(cancellationToken);

        return trip;
    }

    public async Task<bool> DeleteAsync(Guid tripId, CancellationToken cancellationToken)
    {
        int result = await _applicationDbContext.Trips
            .Where(t => t.Id == tripId)
            .ExecuteDeleteAsync(cancellationToken);

        return result > 0;
    }
}