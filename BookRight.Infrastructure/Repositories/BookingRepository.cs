using Microsoft.EntityFrameworkCore;
using BookRight.Domain.Aggregates.Booking;
using BookRight.UseCases.Interfaces;
using BookRight.Domain.Enums;
using BookRight.Domain.ValueObjects;
using System.Reflection.Metadata.Ecma335;
using BookRight.Infrastructure.Persistence;

namespace BookRight.Infrastructure.Repositories
{
    public class BookingRepository : IBookingRepository
    {
    
        private readonly BookRightDbContext _context;
        public BookingRepository(BookRightDbContext context)
        {
            _context = context;
        }

        public async Task<Booking?> GetByIdAsync(Guid bookingId) // async/await is used because database operations are I/O-bound and should not block the executing thread.
        {
            return await _context.Bookings
                .Include(b => b.Lines)//Include() performs eager loading of related booking lines.
                .FirstOrDefaultAsync(b => b.Id == bookingId);
        }

        public async Task<IReadOnlyList<Booking>> GetAllAsync()
        {
            return await _context.Bookings
                .Include(b => b.Lines)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Booking>> GetByWeekAsync(DateOnly weekStart)
        {
            var start = weekStart.ToDateTime(TimeOnly.MinValue);
            var end = start.AddDays(7);

            return await _context.Bookings
                .Include(b => b.Lines)
                .Where(b => b.TimeSlot.StartTime >= start &&
                       b.TimeSlot.StartTime < end)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Booking>> GetByCustomerIdAsync(Guid customerId)
        {
            return await _context.Bookings
                .AsNoTracking()
                .Include(b => b.Lines)
                .Where(b => b.CustomerId == customerId)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Booking>> GetByTherapistIdAsync(Guid therapistId)
        {
            return await _context.Bookings
                .AsNoTracking()
                .Include(b => b.Lines)
                .Where(b => b.TherapistId == therapistId)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Booking>> GetAllBookingsByCustomerIdAsync(Guid customerId)
        {
            return await _context.Bookings
                .AsNoTracking()
                .Include(b => b.Lines)
                .Where(b => b.CustomerId == customerId && b.Status == BookingStatus.Completed)
                .ToListAsync();
        }

        // Count the number of participants for a specific TherapistTreatmentType and TimeSlot, excluding cancelled and no-show bookings.
        public async Task<int> CountParticipantsAsync(Guid therapistTreatmentTypeId, TimeSlot timeSlot)
        {
            return await _context.Bookings
                .Where(b =>
                    b.Status != BookingStatus.Cancelled &&
                    b.Status != BookingStatus.NoShow &&
                    b.Lines.Any(l => l.TherapistTreatmentTypeId == therapistTreatmentTypeId) &&
                    b.TimeSlot.StartTime == timeSlot.StartTime
                )
                .CountAsync();
            
        }

        public async Task CreateAsync(Booking booking)
        {
            await _context.Bookings.AddAsync(booking);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Booking booking)
        {
            _context.Bookings.Update(booking);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid bookingId)
        {
            var existingbooking = await GetByIdAsync(bookingId);
            if(existingbooking != null)
            {
                _context.Bookings.Remove(existingbooking);
                await _context.SaveChangesAsync();
            }
        }

    }
}
