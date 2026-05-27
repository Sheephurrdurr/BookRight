using BookRight.Facade.DTOs.GetClinicByIdDTOs;

namespace BookRight.Facade.Interfaces.ClinicsUseCases
{
    public interface IGetClinicByIdUseCase
    {
        Task<GetClinicByIdResponse> ExecuteAsync(Guid clinicId);
    }
}