using BookRight.Facade.DTOs.GetAllClinicsDTOs;

namespace BookRight.Facade.Interfaces.ClinicsUseCases
{
    public interface IGetAllClinicsUseCase
    {
        Task<IReadOnlyList<GetAllClinicsResponse>> ExecuteAsync();
    }
}
