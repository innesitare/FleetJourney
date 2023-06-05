using FleetJourney.Application.Messages.Notifications.CarPool;
using FleetJourney.Application.Repositories.Abstractions;
using FleetJourney.Domain.CarPool;
using FleetJourney.Domain.Messages.CarPool;
using MassTransit;
using Mediator;

namespace FleetJourney.Application.Messages.Commands.CarPool.Handlers;

public sealed class UpdateCarCommandHandler : ICommandHandler<UpdateCarCommand, Car?>
{
    private readonly ICarPoolRepository _carPoolRepository;
    private readonly ISendEndpointProvider _sendEndpointProvider;
    private readonly IPublisher _publisher;

    public UpdateCarCommandHandler(ICarPoolRepository carPoolRepository, ISendEndpointProvider sendEndpointProvider,
        IPublisher publisher)
    {
        _carPoolRepository = carPoolRepository;
        _sendEndpointProvider = sendEndpointProvider;
        _publisher = publisher;
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
            
            await _publisher.Publish(new UpdateCarMessage
            {
                Car = updatedCar
            }, cancellationToken);
        }

        return updatedCar;
    }
}