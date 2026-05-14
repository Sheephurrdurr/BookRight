using BookRight.Domain.Aggregates.Customer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.Facade.Interfaces
{
	public interface ICusotmerRepository
	{
		Task<IReadOnlyList<Customer>> GetAllAsync();
		Task<Customer?> GetByIdAsync(Guid id);
		Task<bool> ExistsByEmailAsync(string email);
		Task AddAsync(Customer customer);
		Task SaveAsync();
	}
}
