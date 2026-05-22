using BookRight.Domain.Aggregates.TreatmentType;

namespace BookRight.UseCases.Interfaces
{
	public interface ITreatmentTypeRepository
	{
		Task<TreatmentType?> GetByIdAsync(Guid id);
		Task<IEnumerable<TreatmentType>> GetAllAsync();
		Task<IEnumerable<TreatmentType>> GetByIdsAsync(IEnumerable<Guid> therapistTreatmentTypeIds);
		Task AddAsync(TreatmentType treatmentType);
		void Update(TreatmentType treatmentType);
		void Delete(TreatmentType treatmentType);
		Task<bool> SaveChangesAsync();
	}
}
