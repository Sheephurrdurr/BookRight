using BookRight.Facade.DTOs.GetAllTreatmentTypesDTOs;

namespace BookRight.Facade.Interfaces.TreatmentTypesUseCase
{
    public interface IGetAllTreatmentTypesUseCase
    {
        Task<IReadOnlyList<GetAllTreatmentTypesResponse>> ExecuteAsync();
    }
}
