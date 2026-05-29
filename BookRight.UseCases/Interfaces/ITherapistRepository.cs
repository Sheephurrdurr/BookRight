using BookRight.Domain.Aggregates.TherapistAggregate;

namespace BookRight.UseCases.Interfaces
{
    public interface ITherapistRepository
    {
        Task<Therapist?> GetByIdAsync(Guid id);
        Task UpdateAsync(Therapist therapist);
        Task<bool> ExistsByEmailAsync(string email);
        Task<IReadOnlyList<Therapist>> GetAllAsync();
        Task AddAsync(Therapist therapist);
        Task SaveAsync();
     
    }
}
