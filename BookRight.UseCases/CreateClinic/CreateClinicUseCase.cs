using BookRight.Domain.Aggregates.Clinic;
using BookRight.Domain.ValueObjects;
using BookRight.Facade.DTOs.CreateClinicDTOs;
using BookRight.Facade.Interfaces.ClinicsUseCases;
using BookRight.UseCases.Interfaces;

namespace BookRight.UseCases.CreateClinic
{
    public class CreateClinicUseCase : ICreateClinicUseCase
    {
        private readonly IClinicRepository _repository;

        public CreateClinicUseCase(IClinicRepository repository)
        {
            _repository = repository;
        }

        public async Task<CreateClinicResponse> ExecuteAsync(CreateClinicRequest request)
        {
            var clinic = new Clinic(
                request.Name,
                new Address(request.Street, request.City, request.PostalCode),
                new PhoneNumber(request.Phone),
                request.NumTreatmentRooms);

            await _repository.CreateAsync(clinic);

            return new CreateClinicResponse(clinic.Id);
        }
    }
}