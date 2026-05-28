using BookRight.Domain.Exceptions;
using BookRight.Domain.ValueObjects;
using BookRight.Facade.DTOs.UpdateClinicDTOs;
using BookRight.Facade.Interfaces.ClinicsUseCases;
using BookRight.UseCases.Interfaces;

namespace BookRight.UseCases.UpdateClinic
{
    public class UpdateClinicUseCase : IUpdateClinicUseCase
    {
        private readonly IClinicRepository _clinicRepository;

        public UpdateClinicUseCase(IClinicRepository clinicRepository)
        {
            _clinicRepository = clinicRepository;
        }

        public async Task ExecuteAsync(UpdateClinicRequest request)
        {
            // Find the existing clinic by id.
            var clinic = await _clinicRepository.GetByIdAsync(request.ClinicId);

            // If the clinic does not exist, stop the update.
            if (clinic is null)
                throw new ClinicNotFoundException(request.ClinicId);

            // Update the clinic with the new values from the request.
            clinic.UpdateClinic(
                request.Name,
                new Address(request.Street, request.City, request.PostalCode),
                new PhoneNumber(request.Phone),
                request.NumTreatmentRooms);

            // Save the updated clinic in the database.
            await _clinicRepository.UpdateAsync(clinic);
        }
    }
}