using BookRight.Facade.DTOs.MarkBookingCompleted;

namespace BookRight.Facade.Interfaces.BookingUseCases
{
    public interface IMarkBookingCompletedUseCase
    {
        public Task ExecuteAsync(MarkBookingCompletedRequest request);
    }
}
