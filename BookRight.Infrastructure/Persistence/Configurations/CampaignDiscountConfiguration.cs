using BookRight.Domain.Aggregates.CampaignDiscount;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookRight.Infrastructure.Persistence.Configurations
{
    public class CampaignDiscountConfiguration : IEntityTypeConfiguration<CampaignDiscount>
    {
        public void Configure (EntityTypeBuilder<CampaignDiscount> builder) 
        {
            builder.Property(c => c.Id)
                .ValueGeneratedNever();

            builder.OwnsOne(c => c.DateRange, dr =>
            {
                dr.Property(d => d.StartDate)
                    .HasColumnName("StartDate"); // A little explicit,but EF Core would name the column "DateRange_StartDate". No thanks.

                dr.Property(d => d.EndDate)
                    .HasColumnName("EndDate");
            });
        }
    }
}
