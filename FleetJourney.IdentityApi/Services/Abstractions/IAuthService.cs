namespace FleetJourney.IdentityApi.Services.Abstractions;

public interface IAuthService<in TUser>
{
    Task<string?> RegisterAsync(TUser user, CancellationToken cancellationToken);
    
    Task<string?> LoginAsync(TUser user, CancellationToken cancellationToken);
    
    Task<string?> LoginAsync(string email, string password, CancellationToken cancellationToken);
}