using BookRight.Facade.DTOs.CreateTherapistDTOs;

namespace BookRight.Facade.Interfaces
{
    public interface ICreateTherapistUseCase
    {
        Task<CreateTherapistResponse> ExecuteAsync(CreateTherapistRequest request);
    }
}
