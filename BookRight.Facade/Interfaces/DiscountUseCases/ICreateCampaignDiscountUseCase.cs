using BookRight.Facade.DTOs.CreateCampaignDTOs;

namespace BookRight.Facade.Interfaces.DiscountUseCases
{
    public interface ICreateCampaignDiscountUseCase
    {
        Task<CreateCampaignDiscountResponse> ExecuteAsync(CreateCampaignDiscountRequest request);
    }
}
