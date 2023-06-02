using FleetJourney.IdentityApi.Mapping;
using FleetJourney.IdentityApi.Messages.Commands;
using FleetJourney.IdentityApi.Models;
using FleetJourney.IdentityApi.Services.Abstractions;
using Mediator;

namespace FleetJourney.IdentityApi.Messages.Handlers;

internal sealed class RegisterApplicationUserCommandHandler : ICommandHandler<RegisterApplicationUserCommand, string?>
{
    private readonly IAuthService<ApplicationUser> _authService;

    public RegisterApplicationUserCommandHandler(IAuthService<ApplicationUser> authService)
    {
        _authService = authService;
    }

    public async ValueTask<string?> Handle(RegisterApplicationUserCommand command, CancellationToken cancellationToken)
    {
        var user = command.Request.ToUser();
        string? token = await _authService.RegisterAsync(user, cancellationToken);
        
        return token;
    }
}