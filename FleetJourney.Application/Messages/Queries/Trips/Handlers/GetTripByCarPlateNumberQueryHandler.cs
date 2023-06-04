using FleetJourney.Application.Repositories.Abstractions;
using FleetJourney.Domain.Trips;
using Mediator;

namespace FleetJourney.Application.Messages.Queries.Trips.Handlers;

public sealed class GetTripByCarPlateNumberQueryHandler : IQueryHandler<GetTripByCarPlateNumberQuery, Trip?>
{
    private readonly ITripRepository _tripRepository;

    public GetTripByCarPlateNumberQueryHandler(ITripRepository tripRepository)
    {
        _tripRepository = tripRepository;
    }

    public async ValueTask<Trip?> Handle(GetTripByCarPlateNumberQuery query, CancellationToken cancellationToken)
    {
        var trip = await _tripRepository.GetByCarPlateNumberAsync(query.LicensePlateNumber, cancellationToken);

        return trip;
    }
}