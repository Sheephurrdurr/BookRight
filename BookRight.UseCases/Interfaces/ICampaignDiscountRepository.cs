using BookRight.Domain.Aggregates.CampaignDiscount;

namespace BookRight.UseCases.Interfaces
{
    public interface ICampaignDiscountRepository
    {
        Task<CampaignDiscount?> GetByIdAsync(Guid campaignDiscountId);

        Task<IReadOnlyList<CampaignDiscount>> GetAllAsync();
        
        Task<IReadOnlyList<CampaignDiscount>> GetActiveAsync(DateOnly date); //Retrieves all active campaign discounts for a specific date
        
        Task CreateAsync(CampaignDiscount campaignDiscount); //Creates a new campaign discount

        Task UpdateAsync(CampaignDiscount campaignDiscount);

        Task DeleteAsync(Guid campaignDiscountId);
    }
}