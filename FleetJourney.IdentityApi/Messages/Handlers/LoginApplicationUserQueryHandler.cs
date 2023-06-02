using FleetJourney.IdentityApi.Messages.Queries;
using FleetJourney.IdentityApi.Models;
using FleetJourney.IdentityApi.Services.Abstractions;
using Mediator;

namespace FleetJourney.IdentityApi.Messages.Handlers;

internal sealed class LoginApplicationUserQueryHandler : IQueryHandler<LoginApplicationUserQuery, string?>
{
    private readonly IAuthService<ApplicationUser> _authService;

    public LoginApplicationUserQueryHandler(IAuthService<ApplicationUser> authService)
    {
        _authService = authService;
    }

    public async ValueTask<string?> Handle(LoginApplicationUserQuery query, CancellationToken cancellationToken)
    {
        var request = query.Request;
        string? token = await _authService.LoginAsync(request.Username, request.Password, cancellationToken);

        return token;
    }
}