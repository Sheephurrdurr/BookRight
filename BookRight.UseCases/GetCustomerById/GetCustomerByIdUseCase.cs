using BookRight.Facade.DTOs.GetCustomerByIdDTOs;
using BookRight.UseCases.Interfaces;

namespace BookRight.UseCases.GetCustomerById
{
    public class GetCustomerByIdUseCase
    {
        private readonly ICustomerRepository _repository;

        public GetCustomerByIdUseCase(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetCustomerByIdResponse> ExecuteAsync(Guid customerId)
        {
            var customer = await _repository.GetByIdAsync(customerId);

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
