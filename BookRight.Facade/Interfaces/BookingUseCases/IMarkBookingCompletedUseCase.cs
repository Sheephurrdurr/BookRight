using BookRight.Facade.DTOs.MarkBookingCompletedDTOs;

namespace BookRight.Facade.Interfaces.BookingUseCases
{
    public interface IMarkBookingCompletedUseCase
    {
        public Task ExecuteAsync(MarkBookingCompletedRequest request);
    }
}
