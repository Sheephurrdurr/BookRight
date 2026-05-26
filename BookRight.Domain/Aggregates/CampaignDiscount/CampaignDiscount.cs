using BookRight.Domain.Errors;
using BookRight.Domain.Exceptions;
using BookRight.Domain.ValueObjects;

namespace BookRight.Domain.Aggregates.CampaignDiscount
{
    public class CampaignDiscount
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = null!; //A promise to the EF constructor, that the property is set later. If not it results in a warning.
        public decimal DiscountPercent { get; private set; }
        public DateRange DateRange { get; private set; }

        private readonly List<Guid> _appliesToTreatmentTypeIds = new();
        public IReadOnlyList<Guid> AppliesToTreatmentTypeIds =>
            _appliesToTreatmentTypeIds.AsReadOnly();

        private CampaignDiscount() //EF Core constructor
        {
        } 


        public CampaignDiscount( //Opret kampagne
            string name,
            decimal discountPercent,
            DateRange dateRange,
            IEnumerable<Guid> treatmentTypeIds)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException(
                    DomainErrorMessages.NameCannotBeEmpty,
                    nameof(name));

            if (discountPercent <= 0 || discountPercent > 100) //CustomException
                throw new InvalidPercentageException();

            var ids = treatmentTypeIds?.ToList() ?? new List<Guid>();
            if (ids.Count == 0)
                throw new ArgumentException(
                    DomainErrorMessages.TreatmentTypeIdsMustNotBeEmpty,
                    nameof(treatmentTypeIds));

            Id = Guid.NewGuid();
            Name = name;
            DiscountPercent = discountPercent;
            DateRange = dateRange;
            _appliesToTreatmentTypeIds.AddRange(ids);
        }

        public bool IsActive(DateOnly date) //Tjek om kampagnen er aktiv
        {
            return DateRange.Contains(date); // Tjek udføres af DateRange VO
        }

        public bool AppliesTo(Guid treatmentTypeId)
        {
            return _appliesToTreatmentTypeIds.Contains(treatmentTypeId);
        }
           
    }
}
