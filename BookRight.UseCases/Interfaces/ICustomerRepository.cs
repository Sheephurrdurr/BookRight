using BookRight.Domain.Entities;

namespace BookRight.UseCases.Interfaces
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
