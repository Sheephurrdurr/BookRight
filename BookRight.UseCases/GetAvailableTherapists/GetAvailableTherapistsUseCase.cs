using BookRight.Facade.DTOs.GetAvailableTherapistsDTOs;
using BookRight.Facade.Interfaces.TherapistUseCases;
using BookRight.UseCases.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.UseCases.GetAvailableTherapists
{
    public class GetAvailableTherapistsUseCase : IGetAvailableTherapistsUseCase
    {
        private readonly IClinicRepository _clinicRepository;
        private readonly ITherapistRepository _therapistRepository;

        public GetAvailableTherapistsUseCase(
            IClinicRepository clinicRepository,
            ITherapistRepository therapistRepository)
        {
            _clinicRepository = clinicRepository;
            _therapistRepository = therapistRepository;
        }

        public async Task<List<AvailableTherapistResponse>> ExecuteAsync(GetAvailableTherapistsRequest request)
        {
            // 1. Hent klinikken (VIGTIGT: Husk at .Include(c => c.TherapistSchedules) i jeres repo-impl!)
            var clinic = await _clinicRepository.GetByIdAsync(request.ClinicId);
            if (clinic == null)
                throw new KeyNotFoundException("Klinikken blev ikke fundet.");

            // 2. Find de TherapistIds der har en vagtplan på datoen, hvor IsWorking er true
            var workingTherapistIds = clinic.TherapistSchedules
                .Where(ts => ts.Date == request.Date && ts.IsWorking)
                .Select(ts => ts.TherapistId)
                .ToList();

            // 3. Hent alle behandlere (eller lav en GetByIdsAsync i jeres repo, hvis I vil optimere)
            var allTherapists = await _therapistRepository.GetAllAsync();

            // 4. Filtrer listen af alle behandlere, så vi kun har dem, der er på arbejde i klinikken
            var availableTherapists = allTherapists
                .Where(t => workingTherapistIds.Contains(t.Id))
                .Select(t => new AvailableTherapistResponse
                {
                    TherapistId = t.Id,
                    Name = t.Name.ToString() // Antager jeres Therapist har en .Name property
                })
                .ToList();

            return availableTherapists;
        }
    }
}
