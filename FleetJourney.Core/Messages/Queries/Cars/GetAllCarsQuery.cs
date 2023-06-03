using FleetJourney.Domain.CarPool;
using Mediator;

namespace FleetJourney.Core.Messages.Queries.Cars;

public sealed class GetAllCarsQuery : IQuery<IEnumerable<Car>>
{
}