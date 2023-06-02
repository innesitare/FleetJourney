using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FleetJourney.Core.Settings;
using FleetJourney.IdentityApi.Models;
using FleetJourney.IdentityApi.Services.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace FleetJourney.IdentityApi.Services;

internal sealed class TokenWriter : ITokenWriter<ApplicationUser>
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly JwtSettings _jwtSettings;

    public TokenWriter(IServiceScopeFactory scopeFactory, IOptions<JwtSettings> jwtSettings)
    {
        _scopeFactory = scopeFactory;
        _jwtSettings = jwtSettings.Value;
    }

    public async Task<string> WriteTokenAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
        var claimFactory = _scopeFactory.CreateScope()
            .ServiceProvider
            .GetService<IUserClaimsPrincipalFactory<ApplicationUser>>();

        var claimsPrincipal = await claimFactory!.CreateAsync(user);
        string token = await WriteTokenAsync(claimsPrincipal, cancellationToken);

        return token;
    }

    public Task<string> WriteTokenAsync(ClaimsPrincipal claimsPrincipal, CancellationToken cancellationToken)
    {
        var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var signingCred = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256Signature);
        var jwtSecurityToken = new JwtSecurityToken(_jwtSettings.Issuer,
            _jwtSettings.Audience,
            claimsPrincipal.Claims,
            DateTime.UtcNow,
            DateTime.UtcNow.AddMinutes(_jwtSettings.Expire),
            signingCred);

        return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken));
    }
}