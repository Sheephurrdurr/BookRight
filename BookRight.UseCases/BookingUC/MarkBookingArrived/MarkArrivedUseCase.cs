using BookRight.Facade.DTOs.MarkBookingAsArrivedDTOs;
using BookRight.Facade.Interfaces.BookingUseCases;
using BookRight.UseCases.Interfaces;

namespace BookRight.UseCases.BookingUC.MarkBookingArrived
{
    public class MarkArrivedUseCase : IMarkBookingArrivedUseCase
    {
        private readonly IBookingRepository _bookingRepository;

        // Constructor injection for the booking repository
        public MarkArrivedUseCase(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        // Executes the use case to mark a booking as a Arrived.
        public async Task ExecuteAsync(MarkBookingArrivedRequest request) // Asynchronous method to execute the use case, hvilket tillader ikke-blokerende operationer og forbedrer applikationens ydeevne.
        {
            var booking = await _bookingRepository.GetByIdAsync(request.BookingId);

            if (booking is null)
                throw new KeyNotFoundException("Booking was not found.");

            booking.MarkAsArrived();

            await _bookingRepository.UpdateAsync(booking);
        }
    }
}
