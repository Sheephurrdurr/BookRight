using BookRight.Domain.Exceptions;
using BookRight.Facade.DTOs.GetAllTherapistsDTOs;
using BookRight.Facade.Interfaces;
using BookRight.UseCases.Interfaces;

namespace BookRight.UseCases.GetallTherapists
{
    public class GetAllTherapistsUseCase : IGetAllTherapistsUseCase
    {
        private readonly ITherapistRepository _repository;
        private readonly IClinicRepository _clinicRepository;

        public GetAllTherapistsUseCase(ITherapistRepository repository, IClinicRepository clinicRepository)
        {
            _repository = repository;
            _clinicRepository = clinicRepository;
        }

        public async Task<IReadOnlyList<GetAllTherapistsResponse>> ExecuteAsync()
        {
            var therapists = await _repository.GetAllAsync();
            var clinics = await _clinicRepository.GetAllAsync();

            var clinicDict = clinics.ToDictionary(c => c.Id); //Convert clinics into a dictionary for fast lookup by ClinicId

            return therapists.Select(t =>
            {
                if (!clinicDict.TryGetValue(t.ClinicId, out var clinic))
                    throw new ClinicNotFoundException(t.ClinicId);

                return new GetAllTherapistsResponse(
                    t.Id,
                    t.Name.FirstName,
                    t.Name.LastName,
                    t.Email.Value,
                    t.Specialization,
                    t.Authorization.Type,
                    t.Authorization.Number,
                    t.ClinicId,
                    clinic.Name
                );
            }).ToList();
        }
    }
}

