using BookRight.Facade.DTOs.GetAllCustomersDTOs;

namespace BookRight.Facade.Interfaces
{
    public interface IGetAllCustomersUseCase
    {
        Task<IReadOnlyList<GetAllCustomersResponse>> ExecuteAsync();
    }
}
