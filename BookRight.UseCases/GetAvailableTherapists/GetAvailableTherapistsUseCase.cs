using BookRight.Domain.Exceptions;
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
            // Hent klinikken
            var clinic = await _clinicRepository.GetByIdAsync(request.ClinicId);
            if (clinic == null)
                throw new ClinicNotFoundException(request.ClinicId);

            //Find de TherapistIds der har en vagtplan på datoen, hvor IsWorking er true
            var workingTherapistIds = clinic.TherapistSchedules
                .Where(ts => ts.Date == request.Date && ts.IsWorking)
                .Select(ts => ts.TherapistId)
                .ToList();

            //Hent alle behandlere (eller GetByIdsAsync i repo for optimering)
            var allTherapists = await _therapistRepository.GetAllAsync();

            //Filtrerer listen af alle behandlere, så vi kun har dem, der er på arbejde i klinikken
            var availableTherapists = allTherapists
                .Where(t => workingTherapistIds.Contains(t.Id))
                .Select(t => new AvailableTherapistResponse
                {
                    TherapistId = t.Id,
                    Name = t.Name.ToString()
                })
                .ToList();

            return availableTherapists;
        }
    }
}
