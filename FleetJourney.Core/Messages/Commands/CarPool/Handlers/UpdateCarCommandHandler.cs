using FleetJourney.Domain.CarPool;
using FleetJourney.Domain.Messages.CarPool;
using FleetJourney.Infrastructure.Repositories.Abstractions;
using MassTransit;

namespace FleetJourney.Core.Messages.Commands.CarPool.Handlers;

public sealed class UpdateCarCommandHandler : Mediator.ICommandHandler<UpdateCarCommand, Car?>
{
    private readonly ICarPoolRepository _carPoolRepository;
    private readonly ISendEndpointProvider _sendEndpointProvider;

    public UpdateCarCommandHandler(ICarPoolRepository carPoolRepository, ISendEndpointProvider sendEndpointProvider)
    {
        _carPoolRepository = carPoolRepository;
        _sendEndpointProvider = sendEndpointProvider;
    }

    public async ValueTask<Car?> Handle(UpdateCarCommand command, CancellationToken cancellationToken)
    {
        var updatedCar = await _carPoolRepository.UpdateAsync(command.Car, cancellationToken);
        if (updatedCar is not null)
        {
            var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:update-car"));
            await sendEndpoint.Send<UpdateCar>(new
            {
                Car = updatedCar
            }, cancellationToken);
        }
        
        return updatedCar;
    }
}