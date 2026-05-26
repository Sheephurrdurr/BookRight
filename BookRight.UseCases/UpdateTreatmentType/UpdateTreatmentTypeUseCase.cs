using BookRight.Domain.Exceptions;
using BookRight.Domain.ValueObjects;
using BookRight.Facade.DTOs.UpdateTreatmentTypeDTOs;
using BookRight.Facade.Interfaces.TreatmentTypeUseCase;
using BookRight.UseCases.Interfaces;

namespace BookRight.UseCases.UpdateTreatmentType
{
    public class UpdateTreatmentTypeUseCase : IUpdateTreatmentTypeUseCase
    {
        private readonly ITreatmentTypeRepository _treatmentTypeRepository;
        public UpdateTreatmentTypeUseCase(ITreatmentTypeRepository treatmentTypeRepository)
        {
            _treatmentTypeRepository = treatmentTypeRepository;
        }

        public async Task<UpdateTreatmentTypeResponse> ExecuteAsync(UpdateTreatmentTypeRequest request)
        {
            var treatmentType = await _treatmentTypeRepository.GetByIdAsync(request.Id);
            var price = new Money(request.Price);
            if (treatmentType == null)
                throw new TreatmentTypeNotFoundException(request.Id);
            treatmentType.UpdateTreatmentType(
                request.Name,
                request.DurationMinutes,
                request.MaxParticipants,
                price,
                request.CanBeCombined,
                request.RequiredSpecialization);
            _treatmentTypeRepository.Update(treatmentType);
            await _treatmentTypeRepository.SaveChangesAsync();

            return new UpdateTreatmentTypeResponse
            {
                Id = treatmentType.Id,
                Name = treatmentType.Name,
                DurationMinutes = treatmentType.DurationMinutes,
                MaxParticipants = treatmentType.MaxParticipants,
                Price = treatmentType.Price.Value,
                CanBeCombined = treatmentType.CanBeCombined,
                RequiredSpecialization = treatmentType.RequiredSpecialization

            };


        }
    }
}
