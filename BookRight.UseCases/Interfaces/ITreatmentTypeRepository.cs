using BookRight.Domain.Aggregates.TreatmentType;

namespace BookRight.UseCases.Interfaces
{
	public interface ITreatmentTypeRepository
	{
		Task<TreatmentType?> GetByIdAsync(Guid id);
		Task<IEnumerable<TreatmentType>> GetAllAsync();

        // Takes in a list of therapist treatment type IDs and returns a dictionary, mapping each ID to its corresponding TreatmentType
        Task<Dictionary<Guid, TreatmentType>> GetByTherapistTreatmentTypeIdsAsync(IEnumerable<Guid> therapistTreatmentTypeIds);
		Task AddAsync(TreatmentType treatmentType);
		void Update(TreatmentType treatmentType);
		void Delete(TreatmentType treatmentType);
		Task<bool> SaveChangesAsync();
	}
}
