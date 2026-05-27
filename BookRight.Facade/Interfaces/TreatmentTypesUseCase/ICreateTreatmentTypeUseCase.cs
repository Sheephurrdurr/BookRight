using BookRight.Facade.DTOs.CreateTreatmentTypeDTOs;

namespace BookRight.Facade.Interfaces.TreatmentTypeUseCase
{
    public interface ICreateTreatmentTypeUseCase
    {
        Task<CreateTreatmentTypeResponse> ExecuteAsync(CreateTreatmentTypeRequest request);

    }
}
