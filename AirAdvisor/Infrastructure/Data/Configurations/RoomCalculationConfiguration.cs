using Graduation_Project.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Graduation_Project.Infrastructure.Data.Configurations;

public class RoomCalculationConfiguration : IEntityTypeConfiguration<RoomCalculation>
{
    public void Configure(EntityTypeBuilder<RoomCalculation> builder)
    {
        builder.ToTable("RoomCalculations");

        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id).ValueGeneratedOnAdd();
        builder.Property(r => r.UserId).HasMaxLength(450).IsRequired();
        builder.Property(r => r.RecommendedCapacity).HasMaxLength(100);
    }
}

