using FleetJourney.Domain.Trips;
using Mediator;

namespace FleetJourney.Application.Messages.Queries.Trips;

public sealed class GetAllTripsQuery : IQuery<IEnumerable<Trip>>
{
}