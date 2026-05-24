using BookRight.Facade.DTOs.CustomerHistoryDTOs;

namespace BookRight.Facade.Interfaces.CustomerUseCases;

public interface IGetCustomerHistoryUseCase
{
    //Retrieves matching customers with their booking history and loyalty information
    Task<IReadOnlyList<CustomerHistoryResponse>> ExecuteAsync(string query);
}