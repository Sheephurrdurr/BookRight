
namespace BookRight.Facade.DTOs.CreateCampaignDTOs
{
    public sealed record CreateCampaignDiscountResponse
    {
        public Guid Id { get; init; }
        public bool Success { get; init; }
        public string? Message { get; init; }
    }
}
