using FleetJourney.IdentityApi.Contracts.Requests;
using Mediator;

namespace FleetJourney.IdentityApi.Messages.Queries;

internal sealed class LoginApplicationUserQuery : IQuery<string?>
{
    public required LoginRequest Request { get; init; }
}