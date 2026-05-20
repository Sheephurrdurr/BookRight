using BookRight.Facade.DTOs.GetCustomerByIdDTOs;

namespace BookRight.Facade.Interfaces.CustomerUseCases
{
    public interface IGetCustomerByIdUseCase
    {
        Task<GetCustomerByIdResponse> ExecuteAsync(Guid customerId);
    }
}
