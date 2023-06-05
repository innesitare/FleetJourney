using FleetJourney.Domain.CarPool;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetJourney.Infrastructure.Persistence.EntityConfigurations;

public sealed class CarConfiguration : IEntityTypeConfiguration<Car>
{
    public void Configure(EntityTypeBuilder<Car> builder)
    {
        builder.ToTable("CarPool")
            .HasKey(c => c.Id); 

        builder.HasAlternateKey(c => c.LicensePlateNumber);

        builder.Property(c => c.LicensePlateNumber)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(c => c.Brand)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(c => c.Model)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(c => c.EndOfLifeMileage)
            .IsRequired();

        builder.Property(c => c.MaintenanceInterval)
            .IsRequired();

        builder.Property(c => c.CurrentMileage)
            .IsRequired();
        
        builder.HasIndex(t => t.Id)
            .IsUnique()
            .IsDescending(false);

        builder.HasMany(c => c.Trips)
            .WithOne()
            .HasForeignKey(t => t.CarId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}