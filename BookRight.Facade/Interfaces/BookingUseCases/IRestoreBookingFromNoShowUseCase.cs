using BookRight.Facade.DTOs.RestoreBookingFromNoShowDTOs;

namespace BookRight.Facade.Interfaces.BookingUseCases
{
    // Interface for the use case to restore a booking from a NoShow status
    public interface IRestoreBookingFromNoShowUseCase
    {
        Task ExecuteAsync(RestoreBookingFromNoShowRequest request);
    }
}