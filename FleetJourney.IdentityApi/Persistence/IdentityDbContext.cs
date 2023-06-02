using FleetJourney.IdentityApi.Models;
using FleetJourney.IdentityApi.Persistence.Abstractions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FleetJourney.IdentityApi.Persistence;

internal sealed class IdentityDbContext : IdentityDbContext<ApplicationUser>, IIdentityDbContext
{
    public IdentityDbContext(DbContextOptions<IdentityDbContext> dbContextOptions)
        : base(dbContextOptions)
    {
    }   
}