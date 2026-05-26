using BookRight.Domain.Aggregates.TherapistAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.Infrastructure.Persistence.Configurations
{
    public class TherapistScheduleConfiguration : IEntityTypeConfiguration<TherapistSchedule>
    {
        public void Configure(EntityTypeBuilder<TherapistSchedule> builder)
        {

            builder.Property(ts => ts.Id)
                .ValueGeneratedNever();

            // Fortæller EF at BlockedSlots er ejet af TherapistSchedule
            builder.OwnsMany(ts => ts.BlockedSlots, slot =>
            {
                slot.WithOwner();
            });
        }
    }
}
