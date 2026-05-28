using BookRight.Domain.Aggregates.TreatmentType;
using BookRight.Facade.Interfaces.TreatmentTypesUseCase;
using BookRight.UseCases.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.UseCases.AddQualification
{
    public class AddQualificationUseCase : IAddQualificationUseCase
    {
        private readonly ITherapistRepository _therapistRepository;

        public AddQualificationUseCase(ITherapistRepository therapistRepository)
        {
            _therapistRepository = therapistRepository;
        }
        public async Task ExecuteAsync(Guid treatmentTypeId, string requiredSpecialization, decimal basePrice)
        {
            var therapists = await _therapistRepository.GetAllAsync();
            var matchingTherapists = therapists
            .Where(t => t.Authorization.Type.Equals(
                requiredSpecialization,
                StringComparison.OrdinalIgnoreCase))
            .ToList();
            foreach (var therapist in matchingTherapists)
            {
                therapist.AddQualification(treatmentTypeId, basePrice);
                await _therapistRepository.UpdateAsync(therapist);
            }
            await _therapistRepository.SaveAsync();
        }
    }
}
