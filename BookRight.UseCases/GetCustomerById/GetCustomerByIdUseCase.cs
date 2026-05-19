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
            var customers = await _repository.GetByIdAsync(customerId);

            return customers.Select(c => new GetCustomerByIdResponse(
                c.Id,
                c.Name.FirstName,
                c.Name.LastName,
                c.Email.Value,
                c.Phone.Value,
                c.Birth
                
            )).ToList();
        }
    }
}
