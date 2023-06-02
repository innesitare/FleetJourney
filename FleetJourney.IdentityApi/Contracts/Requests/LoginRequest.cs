﻿namespace FleetJourney.IdentityApi.Contracts.Requests;

internal sealed class LoginRequest
{
    public required string Username { get; init; }

    public required string Password { get; init; }
}