using BookRight.Domain.Aggregates.TherapistAggregate;
using BookRight.Domain.Exceptions;
using BookRight.Domain.ValueObjects;
using BookRight.Facade.DTOs.CreateTherapistDTOs;
using BookRight.Facade.Interfaces;
using BookRight.UseCases.Interfaces;

namespace BookRight.UseCases.CreateTherapist
{
    public class CreateTherapistUseCase : ICreateTherapistUseCase
    {
        private readonly ITherapistRepository _repository;
        private readonly ITreatmentTypeRepository _treatmentTypeRepository; // add this

        public CreateTherapistUseCase(ITherapistRepository repository, ITreatmentTypeRepository treatmentTypeRepository )
        {
            _repository = repository;
            _treatmentTypeRepository = treatmentTypeRepository;
        }

        public async Task<CreateTherapistResponse> ExecuteAsync(CreateTherapistRequest request)
        {
            var alreadyExists = await _repository.ExistsByEmailAsync(request.Email);

            if (alreadyExists)
                throw new EmailAlreadyExistsException(request.Email); // Ved ikke lige hvorfor den samme person skulle blive oprettet som medarbejder...

            var treatmentTypes = await _treatmentTypeRepository.GetAllAsync();
            var treatmentTypeDict = treatmentTypes.ToDictionary(t => t.Id);

            var therapist = new Therapist(
                new FullName(request.FirstName, request.LastName),
                new Email(request.Email),
                request.Specialization,
                new Authorization(
                request.AuthorizationType,
                request.AuthorizationNumber),
                request.ClinicId
            );
            foreach (var treatmentTypeId in request.TreatmentTypeIds)
            {
                var treatmentType = treatmentTypeDict[treatmentTypeId];
                therapist.AddQualification(treatmentTypeId, treatmentType.Price.Value);
            }
            await _repository.AddAsync(therapist); 

            return new CreateTherapistResponse
            {
                TherapistId = therapist.Id
             }; 
                                                              
        }
    }
}
