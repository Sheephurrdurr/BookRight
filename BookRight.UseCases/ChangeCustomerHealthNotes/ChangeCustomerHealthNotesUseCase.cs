using BookRight.Domain.Exceptions;
using BookRight.Facade.DTOs.ChangeCustomerHealthNotesDTOs;
using BookRight.Facade.Interfaces.CustomerUseCases;
using BookRight.UseCases.Interfaces;

namespace BookRight.UseCases.ChangeCustomerHealthNotes
{
    public class ChangeCustomerHealthNotesUseCase : IChangeCustomerHealthNotesUseCase
    {
        private readonly ICustomerRepository _repository;

        public ChangeCustomerHealthNotesUseCase(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task ExecuteAsync(ChangeCustomerHealthNotesRequest request)
        {
            var customer = await _repository.GetByIdAsync(request.CustomerId);

            if (customer == null)
                throw new CustomerNotFoundException(request.CustomerId);

            customer.UpdateHealthNotes(request.HealthNotes);

            await _repository.UpdateAsync(customer);
        }
    }
}
