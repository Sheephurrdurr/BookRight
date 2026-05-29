using BookRight.Domain.Aggregates.TreatmentType;
using BookRight.Infrastructure.Persistence;
using BookRight.UseCases.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookRight.Infrastructure.Repositories
{
    public class TreatmentTypeRepository : ITreatmentTypeRepository
    {
        private readonly BookRightDbContext _context;

        public TreatmentTypeRepository(BookRightDbContext context)
        {
            _context = context;
        }
        // hente en TreatmentType ved id
        public async Task<TreatmentType?> GetByIdAsync(Guid id)
        {
            return await _context.TreatmentTypes
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<TreatmentType>> GetAllAsync()
        {
            return await _context.TreatmentTypes
                .ToListAsync();
        }

        // Method returns a dictionary mapping therapist treatment type IDs to their corresponding TreatmentType entities
        public async Task<Dictionary<Guid, TreatmentType>> GetByTherapistTreatmentTypeIdsAsync(IEnumerable<Guid> therapistTreatmentTypeIds)
        {
            var matches = await _context.Therapists // // Go through all Therapists
                .SelectMany(t => t.Qualifications) // Select(many) their Qualifications. SelectMany flattens Qualifications into a single collection
                .Where(ttt => therapistTreatmentTypeIds.Contains(ttt.Id)) // Filter by the provided therapist treatment type IDs
                .ToListAsync(); // Convert to a list

            var treatmentTypeIds = matches.Select(m => m.TreatmentTypeId).ToList(); // extract the TreatmentTypeIds from the matches

            // Query the TreatmentTypes table to get the TreatmentType entities that match the extracted TreatmentTypeIds
            var treatmentTypes = await _context.TreatmentTypes 
                .Where(tt => treatmentTypeIds.Contains(tt.Id))
                .ToListAsync();

            // Create a dictionary that maps the therapist treatment type IDs to their corresponding TreatmentType entities and return it.
            return matches.ToDictionary(
                ttt => ttt.Id,
                ttt => treatmentTypes.First(tt => tt.Id == ttt.TreatmentTypeId)
            );
        }

        public async Task AddAsync(TreatmentType treatmentType)
        {
            await _context.TreatmentTypes.AddAsync(treatmentType);
        }

        public void Update(TreatmentType treatmentType)
        {
            _context.TreatmentTypes.Update(treatmentType);
        }

        public void Delete(TreatmentType treatmentType)
        {
            _context.TreatmentTypes.Remove(treatmentType);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}