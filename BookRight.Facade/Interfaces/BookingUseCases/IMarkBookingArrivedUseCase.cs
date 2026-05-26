
using BookRight.Facade.DTOs.MarkBookingAsArrivedDTOs;

namespace BookRight.Facade.Interfaces.BookingUseCases
{
    public interface IMarkBookingArrivedUseCase
    {
        Task ExecuteAsync(MarkBookingArrivedRequest request); // Method to execute the use case, taking a request object as a parameter
    }
}
