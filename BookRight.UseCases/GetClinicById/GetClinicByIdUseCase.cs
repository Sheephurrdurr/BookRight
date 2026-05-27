using BookRight.Domain.Exceptions;
using BookRight.Facade.DTOs.GetClinicByIdDTOs;
using BookRight.Facade.Interfaces.ClinicsUseCases;
using BookRight.UseCases.Interfaces;

namespace BookRight.UseCases.GetClinicById
{
    public class GetClinicByIdUseCase : IGetClinicByIdUseCase
    {
        private readonly IClinicRepository _repository;

        public GetClinicByIdUseCase(IClinicRepository repository)
        {
            _repository = repository;
        }

        // Retrieves a clinic by its ID and returns a response DTO.
        public async Task<GetClinicByIdResponse> ExecuteAsync(Guid clinicId)
        {
            var clinic = await _repository.GetByIdAsync(clinicId);

            if (clinic is null)
                throw new ClinicNotFoundException(clinicId);

            return new GetClinicByIdResponse(
                clinic.Id,
                clinic.Name,
                clinic.Address.Street,
                clinic.Address.City,
                clinic.Address.PostalCode,
                clinic.Phone.Value,
                clinic.NumTreatmentRooms);
        }
    }
}