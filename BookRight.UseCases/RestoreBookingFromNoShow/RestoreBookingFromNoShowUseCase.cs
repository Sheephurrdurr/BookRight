using BookRight.Domain.Exceptions;
using BookRight.Facade.DTOs.RestoreBookingFromNoShowDTOs;
using BookRight.Facade.Interfaces.BookingUseCases;
using BookRight.UseCases.Interfaces;

namespace BookRight.UseCases.RestoreBookingFromNoShow
{
    // Use case for restoring a NoShow booking back to Confirmed.
    public class RestoreBookingFromNoShowUseCase : IRestoreBookingFromNoShowUseCase
    {
        private readonly IBookingRepository _bookingRepository;

        public RestoreBookingFromNoShowUseCase(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task ExecuteAsync(RestoreBookingFromNoShowRequest request)
        {
            // Find the booking by id.
            var booking = await _bookingRepository.GetByIdAsync(request.BookingId);

            if (booking is null)
                throw new BookingNotFoundException(request.BookingId);

            // Restore status and save changes.
            booking.RestoreFromNoShow();

            await _bookingRepository.UpdateAsync(booking);
        }
    }
}