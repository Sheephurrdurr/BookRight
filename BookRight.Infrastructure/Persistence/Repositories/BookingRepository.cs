using BookRight.Domain.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using BookRight.Domain.Entities;
using BookRight.Infrastructure.Persistence;

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
                .Include(b => b.Customer)
                .FirstOrDefaultAsync(b => b.BookingId == bookingId);
        }

        public async Task<IEnumerable<Booking>> GetAllAsync()
        {
            return await _context.Bookings
                .Include(b => b.Customer)
                .ToListAsync();
        }

        public async Task<IEnumerable<Booking>> GetByCustomerIdAsync(Guid customerId)
        {
            return await _context.Bookings
                .Include(b => b.Customer)
                .Where(b => b.CustomerId == customerId)
                .ToListAsync();
        }

        public async Task CreateBookingAsync(Booking booking)
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
