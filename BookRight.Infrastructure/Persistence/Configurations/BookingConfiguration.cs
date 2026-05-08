using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using BookRight.Domain.Entities;
using BookRight.Infrastructure.Persistence;


namespace BookRight.Infrastructure.Persistence.Configurations
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        { // gemmes identity i database
            builder.ToTable("Bookings");

            builder.HasKey(b => b.BookingId);

            builder.Property(b => b.Date)
                .IsRequired();
            builder.Property(b => b.TimeSlot)
                .IsRequired();
            builder.Property(b => b.Duration)
                .IsRequired()
                .HasConversion<string>();

            builder.HasOne(b => b.Customer)
                .WithMany(c => c.Bookings)
                .HasForeignKey(b => b.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
