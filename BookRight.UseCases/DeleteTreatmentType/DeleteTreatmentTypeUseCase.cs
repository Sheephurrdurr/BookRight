using BookRight.Facade.DTOs.DeleteTreatmentTypeDTOs;
using BookRight.Facade.Interfaces.TreatmentTypesUseCase;
using BookRight.UseCases.Interfaces;

namespace BookRight.UseCases.DeleteTreatmentType
{
    public class DeleteTreatmentTypeUseCase : IDeleteTreatmentTypeUseCase
    {
        private readonly ITreatmentTypeRepository _repository;

        public DeleteTreatmentTypeUseCase(ITreatmentTypeRepository repository)
        {
            _repository = repository;
        }

        public async Task ExecuteAsync(DeleteTreatmentTypeRequest request)
        {
            var treatmentType = await _repository.GetByIdAsync(request.Id);
            _repository.Delete(treatmentType);
            await _repository.SaveChangesAsync();
        }


    }
}
