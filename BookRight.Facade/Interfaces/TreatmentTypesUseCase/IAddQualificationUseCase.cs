using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.Facade.Interfaces.TreatmentTypesUseCase
{
    public interface IAddQualificationUseCase
    {
        Task ExecuteAsync(Guid treatmentTypeId, string requiredSpecialization, decimal basePrice);
    }
}
