using BookRight.Domain.Aggregates.TreatmentType;
using BookRight.Domain.ValueObjects;
using BookRight.Facade.DTOs.CreateTreatmentTypeDTOs;
using BookRight.Facade.Interfaces.TreatmentTypeUseCase;
using BookRight.UseCases.Interfaces;

namespace BookRight.UseCases.CreateTreatmentType
{
    public class CreateTreatmentTypeUseCase : ICreateTreatmentTypeUseCase
    {
        private readonly ITreatmentTypeRepository _treatmentTypeRepository;
        public CreateTreatmentTypeUseCase(ITreatmentTypeRepository treatmentTypeRepository)
        {
            _treatmentTypeRepository = treatmentTypeRepository;
        }

        public async Task<CreateTreatmentTypeResponse> ExecuteAsync(CreateTreatmentTypeRequest request)
        {
            var price = new Money(request.Price);
            var treatmentType = new TreatmentType(
                request.Name,
                request.DurationMinutes,
                request.MaxParticipants,
                price,
                request.CanBeCombined,
                request.RequiredSpecialization);

            await _treatmentTypeRepository.AddAsync(treatmentType);
            return new CreateTreatmentTypeResponse
            {
                TreatmentTypeId = treatmentType.Id
            };

        }
    }
}
