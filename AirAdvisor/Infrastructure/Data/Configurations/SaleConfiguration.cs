using Graduation_Project.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Graduation_Project.Infrastructure.Data.Configurations;

public class SaleConfiguration : IEntityTypeConfiguration<Sale>
{
    public void Configure(EntityTypeBuilder<Sale> builder)
    {
        builder.ToTable("Sales");

        builder.HasKey(s => s.SaleId);
        builder.Property(s => s.SaleId).ValueGeneratedOnAdd();
        builder.Property(s => s.UserId).HasMaxLength(450).IsRequired();
        builder.Property(s => s.TotalPrice).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(s => s.Status).HasConversion<string>().HasMaxLength(20).HasDefaultValue(OrderStatus.Pending);
    }
}

