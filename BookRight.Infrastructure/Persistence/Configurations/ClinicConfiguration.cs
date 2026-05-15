using BookRight.Domain.Aggregates.Clinic;
using BookRight.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookRight.Infrastructure.Persistence.Configurations;

public class ClinicConfiguration : IEntityTypeConfiguration<Clinic>
{
    public void Configure(EntityTypeBuilder<Clinic> builder)
    {
        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.OwnsOne(c => c.Address, address =>
        {
            address.Property(a => a.Street)
                .HasMaxLength(100)
                .IsRequired();

            address.Property(a => a.City)
                .HasMaxLength(100)
                .IsRequired();
        });

        builder.OwnsOne(c => c.Phone, phone =>
        {
            phone.Property(p => p.Value)
                .HasMaxLength(20)
                .IsRequired();
        });

        builder.OwnsMany<OpeningHours>("_openingHours", openingHours =>
        {
            openingHours.ToTable("ClinicOpeningHours");

            openingHours.WithOwner()
                .HasForeignKey("ClinicId");

            openingHours.Property<DayOfWeek>("DayOfWeek")
                .IsRequired();

            openingHours.Property(o => o.OpenTime)
                .IsRequired();

            openingHours.Property(o => o.CloseTime)
                .IsRequired();

            openingHours.HasKey("ClinicId", "DayOfWeek");
        });
    }
}