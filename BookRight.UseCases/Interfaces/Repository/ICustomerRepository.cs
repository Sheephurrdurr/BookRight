using BookRight.Domain.Aggregates.Customer;

namespace BookRight.UseCases.Interfaces.Repository
{
    public interface ICustomerRepository
    {
        Task<Customer?> GetByIdAsync(Guid id);
        Task<bool> ExistsByEmailAsync(string email);
        Task<IReadOnlyList<Customer>> GetAllAsync();
        Task AddAsync(Customer customer);
        Task SaveAsync();
    }
}
