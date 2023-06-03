using FleetJourney.Domain.CarPool;

namespace FleetJourney.Infrastructure.Repositories.Abstractions;

public interface ICarPoolRepository : IRepository<Car, string>
{
}