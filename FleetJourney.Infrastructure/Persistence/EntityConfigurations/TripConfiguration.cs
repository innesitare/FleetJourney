using FleetJourney.Domain.Trips;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetJourney.Infrastructure.Persistence.EntityConfigurations;

public sealed class TripConfiguration : IEntityTypeConfiguration<Trip>
{
    public void Configure(EntityTypeBuilder<Trip> builder)
    {
        builder.ToTable("Trips")
            .HasKey(t => t.Id);

        builder.Property(t => t.StartMileage)
            .IsRequired();
        
        builder.Property(t => t.EndMileage)
            .IsRequired();
        
        builder.Property(t => t.IsPrivateTrip)
            .IsRequired();
        
        builder.HasIndex(e => e.Id)
            .IsUnique()
            .IsDescending(false);

        builder.HasOne(t => t.Employee)
            .WithMany(e => e.Trips)
            .HasForeignKey(t => t.EmployeeId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}