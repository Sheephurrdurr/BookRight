

namespace BookRight.Facade.DTOs.CreateCampaignDTOs
{
    public sealed record CreateCampaignDiscountRequest
    {
        public string Name { get; set; }

        public decimal DiscountPercent { get; init; }
        public DateOnly StartDate { get; init; }
        public DateOnly EndDate { get; init; }
        public List<Guid> TreatmentTypeIds { get; init; }
    }
}
