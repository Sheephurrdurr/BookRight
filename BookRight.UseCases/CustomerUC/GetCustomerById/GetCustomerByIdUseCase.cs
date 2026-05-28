using BookRight.Domain.Exceptions;
using BookRight.Facade.DTOs.GetCustomerByIdDTOs;
using BookRight.Facade.Interfaces.CustomerUseCases;
using BookRight.UseCases.Interfaces;

namespace BookRight.UseCases.CustomerUC.GetCustomerById
{
    public class GetCustomerByIdUseCase : IGetCustomerByIdUseCase
    {
        private readonly ICustomerRepository _repository; // Declare the repository field

        public GetCustomerByIdUseCase(ICustomerRepository repository)
        {
            _repository = repository; // Initialize the repository field through 'constructor injection'
        }

        public async Task<GetCustomerByIdResponse> ExecuteAsync(Guid customerId)
        {
            var customer = await _repository.GetByIdAsync(customerId); // Use the repository method, GetByIdAsync to fetch the customer by ID

            if (customer == null)
            {
                throw new CustomerNotFoundException(customerId);
            }

            // Bundle the data retrieved from the repository into a GetCustomerByIdResponse object and return it
            return new GetCustomerByIdResponse(
                customer.Id,
                customer.Name.FirstName,
                customer.Name.LastName,
                customer.Email.Value,
                customer.Phone.Value,
                customer.DateOfBirth,
                customer.PreferredTherapistId
            ); 
          
        }
    }
}
