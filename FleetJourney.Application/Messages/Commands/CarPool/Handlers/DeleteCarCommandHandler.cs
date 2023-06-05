using FleetJourney.Application.Messages.Notifications.CarPool;
using FleetJourney.Application.Repositories.Abstractions;
using FleetJourney.Domain.Messages.CarPool;
using MassTransit;
using Mediator;

namespace FleetJourney.Application.Messages.Commands.CarPool.Handlers;

public sealed class DeleteCarCommandHandler : ICommandHandler<DeleteCarCommand, bool>
{
    private readonly ICarPoolRepository _carPoolRepository;
    private readonly ISendEndpointProvider _endpointProvider;
    private readonly IPublisher _publisher;

    public DeleteCarCommandHandler(ICarPoolRepository carPoolRepository, ISendEndpointProvider endpointProvider,
        IPublisher publisher)
    {
        _carPoolRepository = carPoolRepository;
        _endpointProvider = endpointProvider;
        _publisher = publisher;
    }

    public async ValueTask<bool> Handle(DeleteCarCommand command, CancellationToken cancellationToken)
    {
        var car = await _carPoolRepository.GetAsync(command.Id, cancellationToken);
        if (car is not null)
        {
            var sendEndpoint = await _endpointProvider.GetSendEndpoint(new Uri("queue:delete-car"));
            await sendEndpoint.Send<DeleteCar>(new
            {
                Id = car.Id
            }, cancellationToken);
            
            await _publisher.Publish(new DeleteCarMessage
            {
                Id = car.Id
            }, cancellationToken);
        }

        bool deleted = await _carPoolRepository.DeleteAsync(command.Id, cancellationToken);
        return deleted;
    }
}