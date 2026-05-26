
using BookRight.Facade.DTOs.GetAllCampaignDiscountsDTOs;

namespace BookRight.Facade.Interfaces.DiscountUseCases
{
    public interface IGetAllCampaignDiscountsUseCase
    {
        Task<IReadOnlyList<GetAllCampaignDiscountsResponse>> ExecuteAsync();
    }
}
