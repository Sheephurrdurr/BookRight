using BookRight.Domain.Aggregates.Clinic;
using BookRight.Domain.Aggregates.TherapistAggregate;
using BookRight.UseCases.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookRight.Infrastructure.Persistence.Repositories
{
    public class TherapistRepository : ITherapistRepository
    {
        private readonly BookRightDbContext _context;

        public TherapistRepository(BookRightDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<Therapist>> GetAllAsync()
        {
            return await _context.Therapists
                .Include(t => t.Qualifications)
                .ToListAsync();
        }
        public async Task<Therapist?> GetByIdAsync(Guid id)
        {
            return await _context.Therapists
            .Include(t => t.Qualifications)
            .FirstOrDefaultAsync(t => t.Id == id); //Include QUlifications to ease edit
        }

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            return await _context.Therapists
                .AnyAsync(t => t.Email.Value == email.ToLowerInvariant());
        }
        public async Task UpdateAsync(Therapist therapist) 
        {
            _context.Therapists.Update(therapist);
            await _context.SaveChangesAsync();
        }

        public async Task AddAsync(Therapist therapist)
        {
            _context.Therapists.Add(therapist);
            await _context.SaveChangesAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
