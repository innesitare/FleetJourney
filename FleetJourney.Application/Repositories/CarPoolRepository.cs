using FleetJourney.Application.Extensions;
using FleetJourney.Application.Repositories.Abstractions;
using FleetJourney.Domain.CarPool;
using FleetJourney.Infrastructure.Persistence.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace FleetJourney.Application.Repositories;

internal sealed class CarPoolRepository : ICarPoolRepository
{
    private readonly IApplicationDbContext _applicationDbContext;

    public CarPoolRepository(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<IEnumerable<Car>> GetAllAsync(CancellationToken cancellationToken)
    {
        bool isEmpty = await _applicationDbContext.Cars.AnyAsync(cancellationToken);
        if (!isEmpty)
        {
            return Enumerable.Empty<Car>();
        }

        return _applicationDbContext.Cars.Include(c => c.Trips);
    }

    public async Task<Car?> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        var car = await _applicationDbContext.Cars.FindAsync(new object?[] {id}, cancellationToken);
        if (car is null)
        {
            return null;
        }

        await _applicationDbContext.Cars.LoadDataAsync(car!, c => c.Trips!);
        return car;
    }

    public async Task<bool> CreateAsync(Car car, CancellationToken cancellationToken)
    {
        bool exists = await _applicationDbContext.Cars.AnyAsync(t => t.LicensePlateNumber == car.LicensePlateNumber, cancellationToken);
        if (exists)
        {
            return false;
        }
        
        await _applicationDbContext.Cars.AddAsync(car, cancellationToken);
        int result = await _applicationDbContext.SaveChangesAsync(cancellationToken);
        
        return result > 0;
    }

    public async Task<Car?> UpdateAsync(Car car, CancellationToken cancellationToken)
    {
        bool isContains = await _applicationDbContext.Cars.ContainsAsync(car, cancellationToken);
        if (!isContains)
        {
            return null;
        }

        _applicationDbContext.Cars.Update(car);
        await _applicationDbContext.SaveChangesAsync(cancellationToken);

        return car;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        int result = await _applicationDbContext.Cars
            .Where(c => c.Id == id)
            .ExecuteDeleteAsync(cancellationToken);

        return result > 0;
    }
}