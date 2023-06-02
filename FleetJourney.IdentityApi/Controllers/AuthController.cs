using FleetJourney.IdentityApi.Contracts.Requests;
using FleetJourney.IdentityApi.Helpers;
using FleetJourney.IdentityApi.Messages.Commands;
using FleetJourney.IdentityApi.Messages.Queries;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace FleetJourney.IdentityApi.Controllers;

[ApiController]
internal sealed class AuthController : ControllerBase
{
    private readonly ISender _sender;

    public AuthController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost(ApiEndpoints.Authentication.Register)]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequest request, CancellationToken cancellationToken)
    {
        string? token = await _sender.Send(new RegisterApplicationUserCommand
        {
            Request = request
        }, cancellationToken);

        return token is not null
            ? Ok(token)
            : BadRequest();
    }

    [HttpPost(ApiEndpoints.Authentication.Login)]
    public async Task<IActionResult> LoginAsync([FromBody] LoginRequest request, CancellationToken cancellationToken)
    {
        string? token = await _sender.Send(new LoginApplicationUserQuery
        {
            Request = request
        }, cancellationToken);

        return token is not null
            ? Ok(token)
            : BadRequest("Invalid credentials. The username or password you entered is incorrect.");
    }
}