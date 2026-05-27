
using BookRight.Facade.DTOs.GetAllTreatmentTypesDTOs;
using BookRight.Facade.Interfaces.TreatmentTypesUseCase;
using BookRight.UseCases.Interfaces;

namespace BookRight.UseCases.GetAllTreatmentTypes
{
    public class GetAllTreatmentTypesUseCase : IGetAllTreatmentTypesUseCase
    {
        private readonly ITreatmentTypeRepository _treatmentTypeRepository;

        public GetAllTreatmentTypesUseCase(ITreatmentTypeRepository treatmentTypeRepository)
        {
            _treatmentTypeRepository = treatmentTypeRepository;
        }

        public async Task<IReadOnlyList<GetAllTreatmentTypesResponse>> ExecuteAsync()
        {
            var treatmentTypes = await _treatmentTypeRepository.GetAllAsync();

            return treatmentTypes
                .Select(t => new GetAllTreatmentTypesResponse
                {
                    Id = t.Id,
                    Name = t.Name,
                    DurationMinutes = t.DurationMinutes,
                    MaxParticipants = t.MaxParticipants,
                    Price = t.Price.Value
                })
                .ToList();
              
        }
    }
}
