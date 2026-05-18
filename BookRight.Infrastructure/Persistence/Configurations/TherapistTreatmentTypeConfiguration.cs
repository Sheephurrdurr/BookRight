using BookRight.Domain.Aggregates.Therapist;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookRight.Infrastructure.Persistence.Configurations
{
    public class TherapistTreatmentTypeConfiguration : IEntityTypeConfiguration<TherapistTreatmentType>
    {
        public void Configure(EntityTypeBuilder<TherapistTreatmentType> builder)
        {
            builder.ToTable("TherapistTreatmentType");

            builder.Property(t => t.Id)
                .ValueGeneratedNever();

            builder.Property(t => t.BasePrice)
                .IsRequired();
        }
    }
}
