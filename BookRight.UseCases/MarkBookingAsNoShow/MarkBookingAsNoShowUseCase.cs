using BookRight.Facade.DTOs.MarkBookingAsNoShowDTOs;
using BookRight.Facade.Interfaces.BookingUseCases;
using BookRight.UseCases.Interfaces;

namespace BookRight.UseCases.MarkBookingAsNoShow
{
    // Implementerer interfacet for at markere en booking som NoShow, hvilket sikrer at
    // denne use case kan bruges i hele applikationen via det definerede interface.
    public class MarkBookingAsNoShowUseCase : IMarkBookingAsNoShowUseCase 
    {
        private readonly IBookingRepository _bookingRepository;

        // Constructor injection for the booking repository
        public MarkBookingAsNoShowUseCase(IBookingRepository bookingRepository) 
        {
            _bookingRepository = bookingRepository;
        }

        // Executes the use case to mark a booking as a NoShow.
        public async Task ExecuteAsync(MarkBookingAsNoShowRequest request) // Asynchronous method to execute the use case, hvilket tillader ikke-blokerende operationer og forbedrer applikationens ydeevne.
        {
            var booking = await _bookingRepository.GetByIdAsync(request.BookingId);

            if (booking is null)
                throw new KeyNotFoundException("Booking was not found.");

            booking.MarkAsNoShow();

            await _bookingRepository.UpdateAsync(booking);
        }
    }
}