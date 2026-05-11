using Microsoft.EntityFrameworkCore;
using BookRight.Domain.Aggregates.Booking;
using BookRight.UseCases.Interfaces.Repository;

namespace BookRight.Infrastructure.Persistence.Repositories
{
    public class BookingRepository : IBookingRepository
    {
    
        private readonly BookRightDbContext _context;
        public BookingRepository(BookRightDbContext context)
        {
            _context = context;
        }

        public async Task<Booking?> GetByIdAsync(Guid bookingId)
        {
            return await _context.Bookings
                .Include(b => b.CustomerId)
                .FirstOrDefaultAsync(b => b.Id == bookingId);
        }

        public async Task<IEnumerable<Booking>> GetAllAsync()
        {
            return await _context.Bookings
                .Include(b => b.CustomerId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Booking>> GetByCustomerIdAsync(Guid customerId)
        {
            return await _context.Bookings
                .Include(b => b.CustomerId)
                .Where(b => b.CustomerId == customerId)
                .ToListAsync();
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
