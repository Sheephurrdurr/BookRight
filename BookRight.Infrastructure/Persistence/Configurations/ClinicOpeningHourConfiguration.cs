using BookRight.Domain.Aggregates.Clinic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookRight.Infrastructure.Persistence.Configurations
{
    // Konfiguration for ClinicOpeningHour entiteten, som definerer åbningstider for klinikker.
    public class ClinicOpeningHourConfiguration : IEntityTypeConfiguration<ClinicOpeningHour>
    {
        // PK konfiguration
        public void Configure(EntityTypeBuilder<ClinicOpeningHour> builder)
        {
            // Definerer Id som primærnøgle
            builder.HasKey(coh => coh.Id);
            // Definerer at ClinicId er påkrævet
            builder.Property(coh => coh.ClinicId)
                .IsRequired();
            // Definerer at Weekday er påkrævet og har en maksimal længde på 20 tegn
            builder.Property(coh => coh.Weekday)
                .IsRequired()
                .HasMaxLength(20);
            // Definerer at OpensAt er påkrævet
            builder.Property(coh => coh.OpensAt)
                .IsRequired();
            // Definerer at ClosesAt er påkrævet
            builder.Property(coh => coh.ClosesAt)
                .IsRequired();
            // Definerer relationen mellem ClinicOpeningHour og Clinic, hvor en Clinic kan have mange ClinicOpeningHours
            builder.HasOne<Clinic>()
                .WithMany()
                .HasForeignKey(coh => coh.ClinicId);
        }
    }
}