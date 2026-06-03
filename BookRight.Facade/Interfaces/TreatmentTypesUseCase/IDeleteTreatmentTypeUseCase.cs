using BookRight.Facade.DTOs.DeleteTreatmentTypeDTOs;

namespace BookRight.Facade.Interfaces.TreatmentTypesUseCase
{
    public interface IDeleteTreatmentTypeUseCase
    {
        Task ExecuteAsync(DeleteTreatmentTypeRequest request);

    }
}
