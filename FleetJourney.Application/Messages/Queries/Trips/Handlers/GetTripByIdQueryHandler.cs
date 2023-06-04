using FleetJourney.Application.Repositories.Abstractions;
using FleetJourney.Domain.Trips;
using Mediator;

namespace FleetJourney.Application.Messages.Queries.Trips.Handlers;

public sealed class GetTripByIdQueryHandler : IQueryHandler<GetTripByIdQuery, Trip?>
{
    private readonly ITripRepository _tripRepository;

    public GetTripByIdQueryHandler(ITripRepository tripRepository)
    {
        _tripRepository = tripRepository;
    }

    public async ValueTask<Trip?> Handle(GetTripByIdQuery query, CancellationToken cancellationToken)
    {
        var trip = await _tripRepository.GetAsync(query.Id, cancellationToken);

        return trip;
    }
}