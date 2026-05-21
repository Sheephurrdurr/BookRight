using BookRight.Domain.Exceptions;
using BookRight.Facade.DTOs.MarkBookingAsNoShowDTOs;
using BookRight.Facade.Interfaces.BookingUseCases;
using BookRight.UseCases.Interfaces;

namespace BookRight.UseCases.MarkBookingAsNoShow
{
    //Interface implementation
    public class MarkBookingAsNoShowUseCase : IMarkBookingAsNoShowUseCase 
    {
        private readonly IBookingRepository _bookingRepository;

        //Constructor injection for BookingRepository
        public MarkBookingAsNoShowUseCase(IBookingRepository bookingRepository) 
        {
            _bookingRepository = bookingRepository;
        }

        //Executes the use case to mark a booking as a NoShow.
        public async Task ExecuteAsync(MarkBookingAsNoShowRequest request) //Asynchronous method to execute the use case, hvilket tillader ikke-blokerende operationer og forbedrer applikationens ydeevne
        {
            var booking = await _bookingRepository.GetByIdAsync(request.BookingId);

            throw new BookingNotFoundException(request.BookingId);

            booking.MarkAsNoShow();

            await _bookingRepository.UpdateAsync(booking);
        }
    }
}