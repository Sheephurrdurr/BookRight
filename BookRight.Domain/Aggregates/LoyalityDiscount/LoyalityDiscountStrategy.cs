using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.Domain.Aggregates.LoyalityDiscount
{
	public class LoyalityDiscountStrategy
	{
	   public Guid Id { get; private set; }
		public string Level { get; private set; } = null;
		public decimal DiscountPercent { get; private set; }
		public decimal MinSpend { get; private set; }
		public decimal MaxSpend { get; private set; }
		
		private LoyalityDiscountStrategy () 
		{ 
		}

		public LoyalityDiscountStrategy(
		string level,
		decimal discountPercent,
		decimal minSpend,
		decimal maxSpend)
		{
			if (string.IsNullOrWhiteSpace(level))
				throw new ArgumentException("Niveau må ikke være tomt.");

			if (discountPercent <= 0 || discountPercent  > 100)
				throw new ArgumentException("Rabat skal være mellem 0 og 100.");

			if(minSpend != decimal.MaxValue && maxSpend < minSpend)
			throw new ArgumentException("Minimumsbeløb må ikke være negativt.");

			if (maxSpend != decimal.MaxValue && maxSpend < minSpend)
				throw new ArgumentException("Maksimumsbeløb må ikke være mindre end minimumsbeløb.");

			Id = Guid.NewGuid();
			Level = level;
			DiscountPercent = discountPercent;
			MinSpend = minSpend;
			MaxSpend = maxSpend;
		}

		// Beregner loyalitetsniveau ud fra kundens samlede køb de seneste 12 måneder
		public static LoyalityDiscountStrategy GetLevelForSpend(decimal totalSpend)
		{
			if (totalSpend >= 25_000)
				return new LoyalityDiscountStrategy("Guld", 15, 25_000, decimal.MaxValue);

			if (totalSpend  >= 10_000)
				return new LoyalityDiscountStrategy("Sølv", 10, 10_001, 25_000);

			if (totalSpend >= 3_000)
				return new LoyalityDiscountStrategy("Bronze", 5, 3_000, 10_000);

			return new LoyalityDiscountStrategy("Ingen", 0, 0, 2_999);
		}

		// beregner den rebatterede pris
		public decimal ApplyDiscount(decimal originalPrice)
		
		{
			if (DiscountPercent == 0)
				return originalPrice;

			return originalPrice *(1 - DiscountPercent / 100);
		}

		// tjekker om kunden kvalificerer til dette niveau
		public bool QualifiesForLevel(decimal totalSpend)
		{
			return totalSpend  >= MinSpend &&
			 (MaxSpend == decimal.MaxValue || totalSpend <= MaxSpend);
		}
	}
}
