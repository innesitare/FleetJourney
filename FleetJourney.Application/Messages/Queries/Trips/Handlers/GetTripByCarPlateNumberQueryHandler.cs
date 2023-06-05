using FleetJourney.Application.Repositories.Abstractions;
using FleetJourney.Domain.Trips;
using Mediator;

namespace FleetJourney.Application.Messages.Queries.Trips.Handlers;

public sealed class GetTripByCarIdQueryHandler : IQueryHandler<GetTripByCarIdQuery, Trip?>
{
    private readonly ITripRepository _tripRepository;

    public GetTripByCarIdQueryHandler(ITripRepository tripRepository)
    {
        _tripRepository = tripRepository;
    }

    public async ValueTask<Trip?> Handle(GetTripByCarIdQuery query, CancellationToken cancellationToken)
    {
        var trip = await _tripRepository.GetByCarIdAsync(query.CarId, cancellationToken);

        return trip;
    }
}