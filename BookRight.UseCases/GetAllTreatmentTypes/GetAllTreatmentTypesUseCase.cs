using BookRight.Facade.DTOs.GetAllTreatmentTypesDTOs;
using BookRight.Facade.Interfaces.TreatmentTypeUseCases;
using BookRight.UseCases.Interfaces;

namespace BookRight.UseCases.GetAllTreatmentTypes
{
    public class GetAllTreatmentTypesUseCase : IGetAllTreatmentTypesUseCase
    {
        private readonly ITreatmentTypeRepository _repository;

        public GetAllTreatmentTypesUseCase(ITreatmentTypeRepository repository)
        {
            _repository = repository;
        }

        public async Task<IReadOnlyList<GetAllTreatmentTypesResponse>> ExecuteAsync()
        {
            var treatmentTypes = await _repository.GetAllAsync();

            //Create response DTOs for all treatment types
            return treatmentTypes.Select(t => new GetAllTreatmentTypesResponse(
                t.Id,
                t.Name,
                t.DurationMinutes,
                t.Price.Value
            )).ToList();
        }
    }
}