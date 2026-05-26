using BookRight.Facade.DTOs.UpdateTreatmentTypeDTOs;

namespace BookRight.Facade.Interfaces.TreatmentTypeUseCase
{
    public interface IUpdateTreatmentTypeUseCase
    {
        Task<UpdateTreatmentTypeResponse> ExecuteAsync(UpdateTreatmentTypeRequest request);
    }
}
