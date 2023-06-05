using FleetJourney.Domain.CarPool;
using Mediator;

namespace FleetJourney.Application.Messages.Queries.Cars;

public sealed class GetAllCarsQuery : IQuery<IEnumerable<Car>>
{
}