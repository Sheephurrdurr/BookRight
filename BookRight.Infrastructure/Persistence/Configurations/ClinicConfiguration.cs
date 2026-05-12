using BookRight.Domain.Aggregates.Clinic;
using BookRight.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookRight.Infrastructure.Persistence.Configurations;

public class ClinicConfiguration : IEntityTypeConfiguration<Clinic>
{
    public void Configure(EntityTypeBuilder<Clinic> builder)
    {
        builder.OwnsOne(t => t.Name, name =>
        {
            name.Property(n => n.FirstName)
                .HasMaxLength(100)
                .IsRequired();

            name.Property(n => n.LastName)
                .HasMaxLength(100)
                .IsRequired();
        });

        builder.OwnsOne(c => c.Address, address =>
        {
            address.Property(a => a.Street)
                .HasMaxLength(100)
                .IsRequired();

            address.Property(a => a.City)
                .HasMaxLength(100)
                .IsRequired();
        });

        builder.Property(c => c.Phone)
            .IsRequired()
            .HasMaxLength(20);

        builder.OwnsOne(c => c.Phone, phone =>
        {
            phone.Property(p => p.Value)
                .HasMaxLength(20)
                .IsRequired();
        });
    }
}