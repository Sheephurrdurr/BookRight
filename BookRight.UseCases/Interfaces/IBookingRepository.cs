using BookRight.Domain.Aggregates.Booking;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.UseCases.Interfaces
{
    public interface IBookingRepository
    {
        Task<Booking?> GetByIdAsync(Guid bookingId);
        Task<IEnumerable<Booking>> GetAllAsync();
        Task<IEnumerable<Booking>> GetByCustomerIdAsync(Guid customerId);
        Task CreateAsync(Booking booking);
        Task UpdateAsync(Booking booking);
        Task DeleteAsync(Guid bookingId);
    }
}
