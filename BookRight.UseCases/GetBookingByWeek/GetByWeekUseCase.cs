using BookRight.Domain.Aggregates.Booking;
using BookRight.Facade.DTOs.GetBookingsByWeekDTOs;
using BookRight.Facade.Interfaces.BookingUseCases;
using BookRight.UseCases.Interfaces;

namespace BookRight.UseCases.GetBookingByWeek
{
    public class GetByWeekUseCase : IGetByWeekUseCase
    {
        private readonly IBookingRepository _bookingRepository;

        public GetByWeekUseCase(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<IReadOnlyList<GetByWeekResponse>> ExecuteAsync(DateOnly weekStart)
        {
            var bookings = await _bookingRepository.GetByWeekAsync(weekStart);

            return bookings.Select(b => new GetByWeekResponse
            {
                BookingId = b.Id,
                CustomerId = b.CustomerId,
                TherapistId = b.TherapistId,
                StartTime = b.TimeSlot.StartTime,
                EndTime = b.TimeSlot.EndTime,
                Status = b.Status.ToString()

            }).ToList();

        }
    }
}
