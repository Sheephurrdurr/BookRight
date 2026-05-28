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
        private readonly ITherapistRepository _therapistRepository;
        public CreateTreatmentTypeUseCase(
            ITreatmentTypeRepository treatmentTypeRepository,
            ITherapistRepository therapistRepository)
        {
            _treatmentTypeRepository = treatmentTypeRepository;
            _therapistRepository = therapistRepository;

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
            await _treatmentTypeRepository.SaveChangesAsync();
            var therapists = await _therapistRepository.GetAllAsync();
            var matchingTherapists = therapists
                .Where(t => t.Authorization.Type.Equals(request.RequiredSpecialization, StringComparison.OrdinalIgnoreCase))
                .ToList();

            foreach (var therapist in matchingTherapists)
            {
                therapist.AddQualification(treatmentType.Id, treatmentType.Price.Value);
                await _therapistRepository.UpdateAsync(therapist);
            }

            await _therapistRepository.SaveAsync();

            return new CreateTreatmentTypeResponse
            {
                TreatmentTypeId = treatmentType.Id
            };


        }
    }
}
