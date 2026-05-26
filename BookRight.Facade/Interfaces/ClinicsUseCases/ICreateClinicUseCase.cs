using BookRight.Facade.DTOs.CreateClinicDTOs;

namespace BookRight.Facade.Interfaces.ClinicsUseCases
{
    public interface ICreateClinicUseCase
    {
        Task<CreateClinicResponse> ExecuteAsync(CreateClinicRequest request);
    }
}