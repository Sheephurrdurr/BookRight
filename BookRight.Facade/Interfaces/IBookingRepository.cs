using BookRight.Domain.Aggregates.Booking;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.Facade.Interfaces
{
	public interface IBookingRepository
	{
		Task<Booking?> GetByIdAsync(Guid bookingId);
		Task<IReadOnlyList<Booking>> GetAllAsync(Guid customerId);
		Task<IReadOnlyList<Booking>> GetByCustomerIdAsync(Guid customerId);
		Task CreateAsync(Booking booking);
		Task UpdateAsync(Booking booking);
		Task Deleteasync(Booking booking);
	}
}
