using BookRight.Domain.Aggregates.TreatmentType;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.UseCases.Interfaces
{
	public interface ITreatmentTypeRepository
	{
		Task<TreatmentType?> GetByIdAsync(int id);
		Task<IEnumerable<TreatmentType>> GetAllAsync();
		Task AddAsync(TreatmentType treatmentType);
		void Update(TreatmentType treatmentType);
		void Delete(TreatmentType treatement);
		Task<bool> SaveChangesAsync();
	}
}
