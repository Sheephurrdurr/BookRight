
namespace BookRight.Facade.DTOs.GetAllCampaignDiscountsDTOs
{
    public sealed record GetAllCampaignDiscountsResponse
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = default!;
        public decimal DiscountPercentage { get; init; }
        public DateOnly StartDate { get; init; }
        public DateOnly EndDate { get; init; }
        public IReadOnlyList<Guid> AppliesToTreatmentTypeIds { get; init; } = default!;
    }
}