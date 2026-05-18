using BookRight.Domain.ValueObjects;
using BookRight.Domain.Aggregates.Customer;
using BookRight.Facade.DTOs.CreateCustomerDTOs;
using BookRight.Facade.Interfaces;
using BookRight.UseCases.Interfaces;

namespace BookRight.UseCases.CreateCustomer
{
    public class CreateCustomerUseCase : ICreateCustomerUseCase
    {
        private readonly Interfaces.ICustomerRepository _repository;

        public CreateCustomerUseCase(Interfaces.ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<CreateCustomerResponse> ExecuteAsync(CreateCustomerRequest request)
        {
            var alreadyExists = await _repository.ExistsByEmailAsync(request.Email);

            if (alreadyExists)
                throw new InvalidOperationException($"A customer with email '{request.Email}' already exists.");

            var customer = new Customer(
            new FullName(request.FirstName, request.LastName),
            new Email(request.Email),
            new PhoneNumber(request.Phone),
            request.DateOfBirth,
            request.HealthNotes,
            request.PreferredTherapistId
            );

            await _repository.AddAsync(customer); 

            return new CreateCustomerResponse(customer.Id);
                                                         
        }
    }
}
