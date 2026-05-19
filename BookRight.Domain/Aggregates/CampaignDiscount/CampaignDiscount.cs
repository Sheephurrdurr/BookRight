using BookRight.Domain.Errors;
using BookRight.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.Domain.Aggregates.CampaignDiscount
{
    public class CampaignDiscount
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = null!; //A promise to the EF constructor, that the property is set later. If not it results in a warning.
        public decimal DiscountPercent { get; private set; }
        public DateOnly StartDate { get; private set; }
        public DateOnly EndDate { get; private set; }


        private CampaignDiscount() //EF Core constructor
        {
        } 


        public CampaignDiscount( //Opret kampagne
            string name,
            decimal discountPercent,
            DateOnly startDate,
            DateOnly endDate)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException(
                    DomainErrorMessages.NameCannotBeEmpty,
                    nameof(name));

            if (discountPercent <= 0 || discountPercent > 100) //CustomException
                throw new InvalidPercentageException();

            if (endDate < startDate)
                throw new ArgumentException(
                    DomainErrorMessages.EndDateCannotBeBeforeStartDate,
                    nameof(endDate));

            Id = Guid.NewGuid();
            Name = name;
            DiscountPercent = discountPercent;
            StartDate = startDate;
            EndDate = endDate;
        }

        public bool IsActive(DateOnly date) //Tjek om kampagnen er aktiv
        {
            return date >= StartDate && date <= EndDate;
        }
    }
}
