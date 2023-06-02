using Microsoft.AspNetCore.Identity;

namespace FleetJourney.IdentityApi.Models;

internal sealed class ApplicationUser : IdentityUser
{
    public required string Name { get; init; }
    
    public required string LastName { get; init; }

    override public required string? UserName { get; set; }

    override public required string? Email { get; set; }

    public required string Password { get; init; }
}