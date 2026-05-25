using BookRight.Facade.DTOs.GetAllTreatmentTypesDTOs;

namespace BookRight.Facade.Interfaces.TreatmentTypeUseCase
{
    public interface IGetAllTreatmentTypeUseCase
    {
        Task<IReadOnlyList<GetAllTreatmentTypesResponse>> ExecuteAsync();
    }
}
