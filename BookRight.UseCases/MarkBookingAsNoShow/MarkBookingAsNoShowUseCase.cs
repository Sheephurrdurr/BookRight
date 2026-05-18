using BookRight.Facade.DTOs.MarkBookingAsNoShowDTOs;
using BookRight.Facade.Interfaces.BookingUseCases;
using BookRight.UseCases.Interfaces;

namespace BookRight.UseCases.MarkBookingAsNoShow
{
    // Use case for registering a NoShow booking.
    public class MarkBookingAsNoShowUseCase : IMarkBookingAsNoShowUseCase
    {
        private readonly IBookingRepository _bookingRepository;

        public MarkBookingAsNoShowUseCase(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        // Executes the use case to mark a booking as a NoShow.
        public async Task ExecuteAsync(MarkBookingAsNoShowRequest request)
        {
            var booking = await _bookingRepository.GetByIdAsync(request.BookingId);

            if (booking is null)
                throw new KeyNotFoundException("Booking was not found.");

            booking.MarkAsNoShow();

            await _bookingRepository.UpdateAsync(booking);
        }
    }
}