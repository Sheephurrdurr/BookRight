using BookRight.Facade.DTOs.UpdateTherapistDTOs;

namespace BookRight.Facade.Interfaces
{
    public interface IUpdateTherapistUseCase
    {
        Task<UpdateTherapistResponse> ExecuteAsync(UpdateTherapistRequest request);
    }
}