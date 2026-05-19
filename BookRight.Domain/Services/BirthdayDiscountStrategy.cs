using BookRight.Domain.Aggregates.Customer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.Domain.Services
{
    public class BirthdayDiscountStrategy
    {
        private readonly decimal _discountPercentage = 0.25m;

        public bool IsApplicable(Customer cutsomer, DateOnly treatmentDate)
        {
            return cutsomer.IsEligibleForBirthdayDiscount(treatmentDate);
        }

        public decimal CalculateDiscount(Customer customer, decimal basePrice, DateOnly treatmentDate)
        {
            if (!IsApplicable(customer, treatmentDate))
                return 0m;
            return basePrice * _discountPercentage;
        }
    }
}
