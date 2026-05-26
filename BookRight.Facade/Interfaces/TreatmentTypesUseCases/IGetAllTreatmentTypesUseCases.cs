using BookRight.Facade.DTOs.GetAllTreatmentTypesDTOs;

namespace BookRight.Facade.Interfaces.TreatmentTypeUseCases
{
    public interface IGetAllTreatmentTypesUseCase
    {
        Task<IReadOnlyList<GetAllTreatmentTypesResponse>> ExecuteAsync();
    }
}