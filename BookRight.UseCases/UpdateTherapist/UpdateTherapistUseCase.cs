using BookRight.Domain.ValueObjects;
using BookRight.Facade.DTOs.UpdateTherapistDTOs;
using BookRight.Facade.Interfaces;
using BookRight.UseCases.Interfaces;
using BookRight.Domain.Exceptions;
using BookRight.Domain.ValueObjects;
using BookRight.Domain.Aggregates.TherapistAggregate;

namespace BookRight.UseCases.UpdateTherapist
{
    public class UpdateTherapistUseCase : IUpdateTherapistUseCase
    {
        private readonly ITherapistRepository _therapistRepository;
        private readonly ITreatmentTypeRepository _treatmentTypeRepository;

        public UpdateTherapistUseCase(
            ITherapistRepository therapistRepository,
            ITreatmentTypeRepository treatmentTypeRepository)
        {
            _therapistRepository = therapistRepository;
            _treatmentTypeRepository = treatmentTypeRepository;
        }

        public async Task<UpdateTherapistResponse> ExecuteAsync(UpdateTherapistRequest request)
        {
            var therapist = await _therapistRepository.GetByIdAsync(request.TherapistId);

            if (therapist == null)
                throw new TherapistNotFoundException(request.TherapistId);

            var treatmentTypes = await _treatmentTypeRepository.GetAllAsync();
            var treatmentTypeDict = treatmentTypes.ToDictionary(t => t.Id);

            // Update therapist details
            therapist.UpdateDetails(
                new FullName(request.FirstName, request.LastName),
                new Email(request.Email),
                request.Specialization,
                new Authorization(
                    request.AuthorizationType,
                    request.AuthorizationNumber),
                request.ClinicId
            );

            // Remove existing qualifications before adding updated ones
            foreach (var qualification in therapist.Qualifications.ToList())
            {
                therapist.RemoveQualification(qualification.TreatmentTypeId);
            }

            // Add updated qualifications
            foreach (var treatmentTypeId in request.TreatmentTypeIds)
            {
                var treatmentType = treatmentTypeDict[treatmentTypeId];

                therapist.AddQualification(
                    treatmentTypeId,
                    treatmentType.Price.Value);
            }

            await _therapistRepository.SaveAsync();

            return new UpdateTherapistResponse(therapist.Id);
        }
    }
}