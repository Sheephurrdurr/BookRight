using System;
using System.Collections.Generic;
using System.Text;
using BookRight.Domain.Aggregates.LoyalityDiscount;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookRight.Infrastructure.Persistence.Configurations
{
	public class LoyalityDiscountStrategyConfiguration : IEntityTypeConfiguration<LoyalityDiscountStrategy>

	{
	   public void Configure(EntityTypeBuilder<LoyalityDiscountStrategy> builder)
	   {
			builder.HasKey(x => x.Id);

			builder.Property(x => x.Level)
			.IsRequired()
			.HasMaxLength(50);

			builder.Property(x => x.DiscountPercent)
			.IsRequired()
			.HasPrecision(5, 2);

			builder.Property(x => x.MinSpend)
			.IsRequired()
			.HasPrecision(18, 2);

			builder.Property(x => x.MaxSpend)
			.IsRequired()
			.HasPrecision(18, 2);
	   }
	}
}
