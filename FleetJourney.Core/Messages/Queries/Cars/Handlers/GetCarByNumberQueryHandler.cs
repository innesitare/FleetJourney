using FleetJourney.Domain.CarPool;
using FleetJourney.Infrastructure.Repositories.Abstractions;
using Mediator;

namespace FleetJourney.Core.Messages.Queries.Cars.Handlers;

public class GetCarByNumberQueryHandler : IQueryHandler<GetCarByNumberQuery, Car?>
{
    private readonly ICarPoolRepository _carPoolRepository;

    public GetCarByNumberQueryHandler(ICarPoolRepository carPoolRepository)
    {
        _carPoolRepository = carPoolRepository;
    }

    public async ValueTask<Car?> Handle(GetCarByNumberQuery query, CancellationToken cancellationToken)
    {
        var cars = await _carPoolRepository.GetAsync(query.LicensePlateNumber, cancellationToken);

        return cars;
    }
}