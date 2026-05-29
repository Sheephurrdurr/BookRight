using BookRight.Domain.Aggregates.CampaignDiscount;
using BookRight.Domain.Aggregates.Clinic;
using BookRight.Domain.Aggregates.TherapistAggregate;
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

            address.Property(a => a.PostalCode)
                .HasMaxLength(100)
                .IsRequired();
        });

        builder.OwnsOne(c => c.Phone, phone =>
        {
            phone.Property(p => p.Value)
                .HasMaxLength(20)
                .IsRequired();
        });

        builder.OwnsMany(c => c.OpeningHours, oh =>
        {
            oh.ToTable("ClinicOpeningHours");
            oh.WithOwner().HasForeignKey(o => o.ClinicId);
            oh.HasKey(o => o.ClinicId);
        });

        builder.HasMany<Therapist>()
                   .WithMany()
                   .HasForeignKey(x => x.ClinicId);

    }
}