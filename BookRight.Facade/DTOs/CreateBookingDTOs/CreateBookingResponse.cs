
namespace BookRight.Facade.DTOs.CreateBookingDTOs
{
    public sealed record CreateBookingResponse
    {
        public Guid Id { get; set; }
        public bool Success { get; set; }
        public string? Message { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal DiscountedPrice { get; set; }
        public string? DiscountType { get; set; }
    }
}
