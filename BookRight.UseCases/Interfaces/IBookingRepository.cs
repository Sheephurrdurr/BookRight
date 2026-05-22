using BookRight.Domain.Aggregates.Booking;

namespace BookRight.UseCases.Interfaces
{
    public interface IBookingRepository
    {
        Task<Booking?> GetByIdAsync(Guid bookingId);
        Task<IReadOnlyList<Booking>> GetAllAsync();
        Task<IReadOnlyList<Booking>> GetByCustomerIdAsync(Guid customerId);
        Task<IReadOnlyList<Booking>> GetAllBookingsByCustomerIdAsync(Guid customerId);
        Task CreateAsync(Booking booking);
        Task UpdateAsync(Booking booking);
        Task DeleteAsync(Guid bookingId);
    }
}
