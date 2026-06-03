using Microsoft.EntityFrameworkCore;
using BookRight.Domain.Aggregates.Customer;
using BookRight.UseCases.Interfaces;
using BookRight.Infrastructure.Persistence;

namespace BookRight.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly BookRightDbContext _context;

        public CustomerRepository(BookRightDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<Customer>> GetAllAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer?> GetByIdAsync(Guid id)
        {
            return await _context.Customers.FindAsync(id);
        }

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            return await _context.Customers
               .AnyAsync(t => t.Email.Value == email.ToLowerInvariant());
        }

        public async Task UpdateAsync(Customer customer)
        {
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
        }

        public async Task AddAsync(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<Customer>> SearchAsync(string query) //Search for customer using email phoneno., first or last name.
        {
            query = query.ToLower().Trim(); //Converts input to lowercase and removes whitespace to make the search case-insensitive.

            return await _context.Customers // async/await used because database operations are I/O-bound and should not block the executing thread
                .Where(c =>
                    c.Email.Value.ToLower().Contains(query) ||
                    c.Phone.Value.Contains(query) ||
                    c.Name.FirstName.ToLower().Contains(query) ||
                    c.Name.LastName.ToLower().Contains(query))
                .ToListAsync();
        }
      
    }
}
