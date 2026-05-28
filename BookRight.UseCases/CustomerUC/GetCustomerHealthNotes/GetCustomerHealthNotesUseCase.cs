using BookRight.Domain.Exceptions;
using BookRight.Facade.DTOs.GetCustomerHealthNotesDTOs;
using BookRight.Facade.Interfaces.CustomerUseCases;
using BookRight.UseCases.Interfaces;

namespace BookRight.UseCases.CustomerUC.GetCustomerHealthNotes
{
    public class GetCustomerHealthNotesUseCase : IGetCustomerHealthNotesUseCase
    {
        private readonly ICustomerRepository _customerRepository;

        public GetCustomerHealthNotesUseCase(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        //This method retrieves the health notes for a specific customer based on their ID
        public async Task<CustomerHealthNotesResponse> ExecuteAsync(Guid customerId)
        {
            var customer = await _customerRepository.GetByIdAsync(customerId);

            if (customer == null)
            throw new CustomerNotFoundException(customerId);

            return new CustomerHealthNotesResponse( 
                customer.Id,
                customer.HealthNotes
            );
        }
    }
}
