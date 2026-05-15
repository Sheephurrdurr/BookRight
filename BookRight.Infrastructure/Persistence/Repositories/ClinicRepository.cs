using Microsoft.EntityFrameworkCore;
using BookRight.Domain.Aggregates.Clinic;
using BookRight.UseCases.Interfaces;

namespace BookRight.Infrastructure.Persistence.Repositories
{
    public class ClinicRepository : IClinicRepository
    {
        private readonly BookRightDbContext _context;

        public ClinicRepository(BookRightDbContext context)
        {
            _context = context;
        }

        public async Task<Clinic?> GetByIdAsync(Guid clinicId) //Gets destinctive clinic by id
        {
            return await _context.Clinics
                .FirstOrDefaultAsync(c => c.Id == clinicId);
        }

        public async Task<IReadOnlyList<Clinic>> GetAllAsync() //All clinics
        {
            return await _context.Clinics
                .ToListAsync();
        }

        public async Task CreateAsync(Clinic clinic) //Create and save new clinic
        {
            await _context.Clinics.AddAsync(clinic);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Clinic clinic) //Update and save
        {
            _context.Clinics.Update(clinic);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid clinicId) //Delete
        {
            var existingClinic = await GetByIdAsync(clinicId);

            if (existingClinic != null)
            {
                _context.Clinics.Remove(existingClinic);
                await _context.SaveChangesAsync();
            }
        }
    }
}