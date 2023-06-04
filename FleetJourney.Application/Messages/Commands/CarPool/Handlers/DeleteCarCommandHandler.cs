using FleetJourney.Application.Repositories.Abstractions;
using FleetJourney.Domain.Messages.CarPool;
using MassTransit;
using Mediator;

namespace FleetJourney.Application.Messages.Commands.CarPool.Handlers;

public sealed class DeleteCarCommandHandler : ICommandHandler<DeleteCarCommand, bool>
{
    private readonly ICarPoolRepository _carPoolRepository;
    private readonly ISendEndpointProvider _endpointProvider;

    public DeleteCarCommandHandler(ICarPoolRepository carPoolRepository, ISendEndpointProvider endpointProvider)
    {
        _carPoolRepository = carPoolRepository;
        _endpointProvider = endpointProvider;
    }

    public async ValueTask<bool> Handle(DeleteCarCommand command, CancellationToken cancellationToken)
    {
        var car = await _carPoolRepository.GetAsync(command.LicensePlateNumber, cancellationToken);
        if (car is not null)
        {
            var sendEndpoint = await _endpointProvider.GetSendEndpoint(new Uri("queue:delete-car"));
            await sendEndpoint.Send<DeleteCar>(new
            {
                car.LicensePlateNumber
            }, cancellationToken);
        }
        
        bool deleted = await _carPoolRepository.DeleteAsync(command.LicensePlateNumber, cancellationToken);
        return deleted;
    }
}