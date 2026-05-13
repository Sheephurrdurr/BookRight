using BookRight.Domain.Aggregates.TreatmentType;
using BookRight.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookRight.Infrastructure.Persistence.Configurations;

public class TreatmentTypeConfiguration : IEntityTypeConfiguration<TreatmentType>
{
    public void Configure(EntityTypeBuilder<TreatmentType> builder)
    {
        // Converts Money to decimal when saving to the database,
        // and converts decimal back to Money when reading from the database.
        builder.Property(t => t.Price)
            .HasConversion(
                money => money!.Value,
                value => new Money(value))
            .IsRequired();
    }
}