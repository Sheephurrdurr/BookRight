using BookRight.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.UseCases.Interfaces
{
    public interface IBookingRepository
    {
        Task CreateAsync(Booking booking);
        Task<Booking?> GetByIdAsync(Guid id);
        Task<List<Booking>> GetAllAsync();
    }
}
