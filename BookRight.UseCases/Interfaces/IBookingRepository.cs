using BookRight.Domain.Aggregates.Booking;
using BookRight.Domain.ValueObjects;

namespace BookRight.UseCases.Interfaces
{
    public interface IBookingRepository
    {
        Task<Booking?> GetByIdAsync(Guid bookingId);
        Task<IReadOnlyList<Booking>> GetAllAsync();
        Task<IReadOnlyList<Booking>> GetByWeekAsync(DateOnly weekStart);
        Task<IReadOnlyList<Booking>> GetByCustomerIdAsync(Guid customerId);
        Task<IReadOnlyList<Booking>> GetAllBookingsByCustomerIdAsync(Guid customerId);
        Task<IReadOnlyList<Booking>> GetByTherapistIdAsync(Guid therapistId);

        // number is used to determine the number of participants in a treatment type at a given time slot
        Task<int> CountParticipantsAsync(Guid therapistTreatmentTypeId, TimeSlot timeSlot);
        Task CreateAsync(Booking booking);
        Task UpdateAsync(Booking booking);
        Task DeleteAsync(Guid bookingId);
    }
}
