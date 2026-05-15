using BookRight.Facade.Interfaces.ClinicsUseCases;
using BookRight.UseCases.Interfaces;
using BookRight.Facade.DTOs.GetAllClinicsDTOs;

namespace BookRight.UseCases.GetAllClinics
{
    public class GetAllClinicsUseCase : IGetAllClinicsUseCase
    {
        private readonly IClinicRepository _clinicRepository;

        public GetAllClinicsUseCase(IClinicRepository clinicRepository)
        {
            _clinicRepository = clinicRepository;
        }

        public async Task<IReadOnlyList<GetAllClinicsResponse>> ExecuteAsync()
        {
            var clinics = await _clinicRepository.GetAllAsync();

            return clinics
                .Select(c => new GetAllClinicsResponse(c.Id, c.Name))
                .ToList();
        }
    }
}
