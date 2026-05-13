using BookRight.Domain.Aggregates.Clinic;

namespace BookRight.UseCases.Interfaces
{
    public interface IClinicRepository
    {
        Task<Clinic?> GetByIdAsync(Guid clinicId); //Destinctive clinic by id

        Task<IReadOnlyList<Clinic>> GetAllAsync(); //All clinics

        Task CreateAsync(Clinic clinic); //Create

        Task UpdateAsync(Clinic clinic); //Update

        Task DeleteAsync(Guid clinicId); //Delete
    }
}