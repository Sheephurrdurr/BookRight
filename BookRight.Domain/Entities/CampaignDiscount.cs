using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.Domain.Entities
{
    public record CampaignDiscount
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public decimal DiscountPercent { get; private set; }
        public DateOnly StartDate { get; private set; }
        public DateOnly EndDate { get; private set; }


        private CampaignDiscount() //EF Core constructor
        {
            Name = string.Empty; //Name må ikke være null. 
        } 


        public CampaignDiscount( //Opret kampagne
            string name,
            decimal discountPercent,
            DateOnly startDate,
            DateOnly endDate)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Navn må ikke være tomt.");

            if (discountPercent <= 0 || discountPercent > 100)
                throw new ArgumentException("Rabat skal være mellem 0 og 100.");

            if (endDate < startDate)
                throw new ArgumentException("Slutdato må ikke være før startdato.");

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
