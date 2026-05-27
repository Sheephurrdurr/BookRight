using BookRight.Facade.DTOs.GetBookingsByWeekDTOs;

namespace BookRight.Facade.Interfaces.BookingUseCases
{
    public interface IGetByWeekUseCase
    {
        Task<IReadOnlyList<GetByWeekResponse>> ExecuteAsync(DateOnly weekStart);
    }
}
