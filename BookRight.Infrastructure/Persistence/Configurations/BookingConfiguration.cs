using BookRight.Domain.Aggregates.Booking;
using BookRight.Domain.Aggregates.CampaignDiscount;
using BookRight.Domain.Aggregates.Customer;
using BookRight.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookRight.Infrastructure.Persistence.Configurations
    {
        public class BookingConfiguration : IEntityTypeConfiguration<Booking>
        {
            public void Configure(EntityTypeBuilder<Booking> builder)
            {
            builder.ToTable("Bookings");

                // PK konfiguration
                builder.Property(x => x.Id)
                    .ValueGeneratedNever();
    
                builder.HasOne<Customer>() // Definér 1 til mange relation
                    .WithMany()
                    .HasForeignKey(x => x.CustomerId)
                    .IsRequired();

                // TimeSlot VO konfiguration
                builder.OwnsOne(b => b.TimeSlot, timeSlot =>
                {
                    timeSlot.Property(t => t.StartTime)
                        .IsRequired();
           
                    timeSlot.Property(t => t.EndTime)
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
                    bl.Property(x => x.BasePrice)// Converts BasePrice from Money to decimal in the database
                        .HasConversion(
                            money => money!.Value,
                            value => new Money(value))
                        .HasPrecision(18, 2)
                        .IsRequired();

                    bl.Property(x => x.FinalPrice)// Converts FinalPrice from Money to decimal in the database
                        .HasConversion(
                            money => money!.Value,
                            value => new Money(value))
                        .HasPrecision(18, 2)
                        .IsRequired();// Er præcis op til 18 cifre, med 2 efter kommaet. Så 16 før kommaet.
                                      // SqlServers bruger som regel den samme præcision som udgangspunkt, men en eksplicit definition minimerer dumme fejl.
                });

                // Status konfiguration
                builder.Property(b => b.Status)
                    .HasConversion<string>()
                    .HasMaxLength(50);

                builder.HasOne<CampaignDiscount>()
                    .WithMany()
                    .HasForeignKey(x => x.CampaignDiscountId);
            }
        }
    }
