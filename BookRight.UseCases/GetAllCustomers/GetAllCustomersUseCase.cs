using BookRight.Facade.DTOs.GetAllCustomersDTOs;
using BookRight.Facade.Interfaces;
using BookRight.UseCases.Interfaces;

namespace BookRight.UseCases.GetAllCustomers
{
    public class GetAllCustomersUseCase : IGetAllCustomersUseCase

    {
        private readonly ICustomerRepository _repository;

        public GetAllCustomersUseCase(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<IReadOnlyList<GetAllCustomersResponse>> ExecuteAsync()
        {
            var customers = await _repository.GetAllAsync();

            return customers.Select(t => new GetAllCustomersResponse(
                t.Id,
                t.Name.FirstName,
                t.Name.LastName,
                t.Email.Value,
                t.Phone.Value
            ))
            .ToList();
        }
    }
}
