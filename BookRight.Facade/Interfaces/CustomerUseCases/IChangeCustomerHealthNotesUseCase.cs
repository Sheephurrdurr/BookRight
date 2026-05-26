using BookRight.Facade.DTOs.ChangeCustomerHealthNotesDTOs;

namespace BookRight.Facade.Interfaces.CustomerUseCases
{
    public interface IChangeCustomerHealthNotesUseCase
    {
        Task ExecuteAsync(ChangeCustomerHealthNotesRequest request);
    }
}
