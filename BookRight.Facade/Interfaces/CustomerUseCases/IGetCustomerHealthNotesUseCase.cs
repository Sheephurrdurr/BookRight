using BookRight.Facade.DTOs.GetCustomerHealthNotesDTOs;

namespace BookRight.Facade.Interfaces.CustomerUseCases
{
    public interface IGetCustomerHealthNotesUseCase
    {
        Task<CustomerHealthNotesResponse> ExecuteAsync(Guid customerId);
    }
}
