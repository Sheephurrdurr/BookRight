using BookRight.Facade.DTOs.GetAllTherapistTreatmentTypesDTOs;

namespace BookRight.Facade.Interfaces.TherapistUseCases
{
    public interface IGetAllTherapistTreatmentTypesUseCase
    {
        Task<IReadOnlyList<GetAllTherapistTreatmentTypesResponse>> ExecuteAsync();
    }
}
