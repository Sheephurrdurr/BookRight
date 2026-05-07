using BookRight.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookRight.Infrastructure.Persistence.Configurations
{
    public class BookingConfiguration : IEntityTypeConfiguration
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.ToTable("Booking");

            // PK konfiguration
            builder.HasKey(b => b.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            // FK CustomerId konfiguration
            builder.Property(b => b.CustomerId)
                .HasConversion(
                    id => id.Value, 
                    value => new CustomerId(value));

            builder.HasOne<Customer>() // Definér 1 til mange relation
                .WithMany()
                .HasForeignKey(x => x.CustomerId)
                .IsRequired();

            // TimeSlot VO konfiguration
            builder.OwnsOne(b => b.TimeSlot, timeSlot =>
            {
                timeSlot.Property(t => t.StartTime)
                    .HasColumnName("StartTime")
                    .HasMaxLength(100)
                    .IsRequired();

                timeSlot.Property(t => t.EndTime)
                    .HasColumnName("EndTime")
                    .HasMaxLength(100)
                    .IsRequired();
            });

            // BookingLine konfiguration
            builder.OwnsMany(b => b.Lines, bl =>
            {
                bl.ToTable("BookingLines");
                bl.WithOwner()
                    .HasForeignKey("BookingId"); // 'shadow property', som kun gemmes i databasen
                bl.Property<Guid>("Id");
                bl.HasKey("Id");

                bl.Property(x => x.TherapistTreatmentTypeId);
                bl.Property(x => x.BasePrice)
                    .HasPrecision(18, 2); // Er præcis op til 18 cifre, med 2 efter kommaet. Så 16 før kommaet.
                                         // SqlServers bruger som regel den samme præcision som udgangspunkt, men en eksplicit definition minimerer dumme fejl.
            });

            // Status konfiguration
            builder.Property(b => b.Status)
                .HasConversion<string>()
                .HasMaxLength(50);

            // FK CampaignDiscount konfiguration
            builder.Property(b => b.campaignDiscountId)
                .HasConversion(
                    id => id.Value,
                    value => new campaignDiscountId(value));

            builder.HasOne<CampaignDiscount>() 
                .WithMany()
                .HasForeignKey(x => x.CampaignDiscountId)
                .IsRequired();
        }
    }
}
