using BookRight.Facade.DTOs.GetBookingsForTodayDTOs;

namespace BookRight.Facade.Interfaces.BookingUseCases
{
    public interface IGetBookingsForTodayUseCase
    {
        Task<IReadOnlyList<GetBookingsForTodayResponse>> ExecuteAsync();
    }
}
