using FleetJourney.Domain.EmployeeInfo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetJourney.Infrastructure.Persistence.EntityConfigurations;

public sealed class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable("Employees")
            .HasKey(e => e.Id);

        builder.Property(e => e.Name)
            .IsRequired()
            .IsUnicode(false)
            .HasMaxLength(50);

        builder.Property(e => e.LastName)
            .IsRequired()
            .IsUnicode(false)
            .HasMaxLength(50);

        builder.Property(e => e.Email)
            .IsRequired()
            .IsUnicode(false)
            .HasMaxLength(50);
        
        builder.Property(e => e.Birthdate)
            .IsRequired();

        builder.HasIndex(e => e.Id)
            .IsUnique()
            .IsDescending(false);

        builder.HasMany(e => e.Trips)
            .WithOne()
            .HasForeignKey(t => t.EmployeeId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}