using BookRight.Facade.DTOs.GetAllTreatmentTypesDTOs;

namespace BookRight.Facade.Interfaces.TreatmentTypeUseCase
{
    public interface IGetAllTreatmentTypesUseCase
    {
        Task<IReadOnlyList<GetAllTreatmentTypesResponse>> ExecuteAsync();
    }
}
