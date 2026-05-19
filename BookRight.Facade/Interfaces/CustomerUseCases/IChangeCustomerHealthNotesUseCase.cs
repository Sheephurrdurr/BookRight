using BookRight.Facade.DTOs.GetCustomerHealthNotesDTOs;

namespace BookRight.Facade.Interfaces.CustomerUseCases
{
    public interface IChangeCustomerHealthNotesUseCase
    {
        Task<CustomerHealthNotesResponse> ExecuteAsync(Guid customerId);
    }
}
