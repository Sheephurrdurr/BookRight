
namespace BookRight.Facade.DTOs.CreateBookingDTOs
{
    public record CreateBookingResponse
    {
        public Guid Id { get; set; }
        public bool Success { get; set; }
        public string? Message { get; set; }
    }
}
