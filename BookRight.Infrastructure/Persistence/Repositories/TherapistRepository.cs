using Microsoft.EntityFrameworkCore;
using BookRight.Domain.Entities;
using BookRight.UseCases.Interfaces;

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
            return await _context.Therapists.ToListAsync();
        }
        public async Task<Therapist?> GetByIdAsync(Guid id)
        {
            return await _context.Therapists.FindAsync(id);
        }

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            return await _context.Therapists
                .AnyAsync(t => t.Email.Value == email.ToLowerInvariant());
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
