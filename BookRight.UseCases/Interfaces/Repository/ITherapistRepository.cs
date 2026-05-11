using BookRight.Domain.Aggregates.Therapist;

namespace BookRight.UseCases.Interfaces.Repository
{
    public interface ITherapistRepository
    {
        Task<Therapist?> GetByIdAsync(Guid id);
        Task<bool> ExistsByEmailAsync(string email);
        Task<IReadOnlyList<Therapist>> GetAllAsync();
        Task AddAsync(Therapist therapist);
        Task SaveAsync();
    }
}
