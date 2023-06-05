using FleetJourney.Domain.CarPool;
using Mediator;

namespace FleetJourney.Application.Messages.Queries.Cars;

public sealed class GetCarByIdQuery : IQuery<Car?>
{
    public required Guid Id { get; init; }
}