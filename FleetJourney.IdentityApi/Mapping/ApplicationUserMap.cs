using FleetJourney.IdentityApi.Contracts.Requests;
using FleetJourney.IdentityApi.Models;
using Mapster;

namespace FleetJourney.IdentityApi.Mapping;

internal static class ApplicationUserMap
{
    public static ApplicationUser ToUser(this RegisterRequest request)
    {
        return request.Adapt<ApplicationUser>();
    }
}