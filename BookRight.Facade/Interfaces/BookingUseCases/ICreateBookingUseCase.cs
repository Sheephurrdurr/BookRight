using BookRight.Facade.DTOs.CreateBookingDTOs;

namespace BookRight.Facade.Interfaces
{
    public interface ICreateBookingUseCase
    {
        Task<CreateBookingResponse> ExecuteAsync(CreateBookingRequest request);
    }
}
