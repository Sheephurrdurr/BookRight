using BookRight.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.UseCases.Interfaces
{
    public interface ITreatmentTypeRepository
    {
        Task<List<TreatmentType>> GetAllAsync();
        Task<TreatmentType?> GetByIdAsync(Guid id);
        Task CreateAsync(TreatmentType treatmentType);
        Task UpdateAsync(TreatmentType treatmentType);
        Task DeleteAsync(Guid id);
    }
}
