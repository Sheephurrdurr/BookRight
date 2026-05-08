using BookRight.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.Domain.Interface
{
    public interface IBookingRepository
    {
        Task<Booking?> GetByIdAsync(Guid bookingId);
        Task<IEnumerable<Booking>> GetAllAsync();
        Task<IEnumerable<Booking>> GetByCustomerIdAsync(Guid customerId);
        Task CreateBookingAsync(Booking booking);
        Task UpdateAsync(Booking booking);
        Task DeleteAsync(Guid bookingId);



    }
}
