using BookRight.Domain.Aggregates.Customer;
using BookRight.Domain.Exceptions;
using BookRight.Domain.ValueObjects;
using BookRight.Facade.DTOs.CreateCustomerDTOs;
using BookRight.Facade.Interfaces;

namespace BookRight.UseCases.CustomerUC.CreateCustomer
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
                throw new EmailAlreadyExistsException(request.Email);

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
