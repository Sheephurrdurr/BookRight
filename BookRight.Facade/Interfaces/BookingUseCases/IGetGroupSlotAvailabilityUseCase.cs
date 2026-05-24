using BookRight.Facade.DTOs.GetGroupSlotAvailabilityDTOs;

namespace BookRight.Facade.Interfaces.BookingUseCases
{
    public interface IGetGroupSlotAvailabilityUseCase
    {
        Task<GetGroupSlotAvailabilityResponse> ExecuteAsync(GetGroupSlotAvailabilityRequest request);
    }
}
