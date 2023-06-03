using FleetJourney.Domain.CarPool;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetJourney.Infrastructure.Persistence.EntityConfigurations;

public sealed class CarConfiguration : IEntityTypeConfiguration<Car>
{
    public void Configure(EntityTypeBuilder<Car> builder)
    {
        builder.ToTable("CarPool")
            .HasKey(c => c.LicensePlateNumber);
        
        builder.Property(c => c.Brand)
            .IsUnicode(false)
            .IsRequired()
            .HasMaxLength(50);
        
        builder.Property(c => c.Model)
            .IsUnicode(false)
            .IsRequired()
            .HasMaxLength(50);
        
        builder.Property(c => c.EndOfLifeMileage)
            .IsRequired();
        
        builder.Property(c => c.MaintenanceInterval)
            .IsRequired();
        
        builder.Property(c => c.CurrentMileage)
            .IsRequired();

        builder.HasMany(c => c.Trips)
            .WithOne(t => t.Car)
            .HasForeignKey(t => t.LicensePlateNumber)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}