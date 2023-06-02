using FleetJourney.IdentityApi.Contracts.Requests;
using Mediator;

namespace FleetJourney.IdentityApi.Messages.Commands;

internal sealed class RegisterApplicationUserCommand : ICommand<string?>
{
    public required RegisterRequest Request { get; init; }
}