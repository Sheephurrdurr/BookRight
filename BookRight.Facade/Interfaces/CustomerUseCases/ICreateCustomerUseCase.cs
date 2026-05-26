using BookRight.Facade.DTOs.CreateCustomerDTOs;

namespace BookRight.Facade.Interfaces
{
    public interface ICreateCustomerUseCase
    {
        Task<CreateCustomerResponse> ExecuteAsync(CreateCustomerRequest request);
    }
}
