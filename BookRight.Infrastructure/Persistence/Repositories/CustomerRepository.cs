using Microsoft.EntityFrameworkCore;
using BookRight.Domain.Entities;
using BookRight.UseCases.Interfaces;

namespace BookRight.Infrastructure.Persistence.Repositories
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

        public async Task AddAsync(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
