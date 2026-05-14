using BookRight.Domain.Aggregates.CampaignDiscount;
using BookRight.UseCases.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookRight.Infrastructure.Persistence.Repositories
{
    public class CampaignDiscountRepository : ICampaignDiscountRepository
    {
        private readonly BookRightDbContext _context;

        public CampaignDiscountRepository(BookRightDbContext context)
        {
            _context = context;
        }

        // Retrieves a campaign discount by its unique identifier
        public async Task<CampaignDiscount?> GetByIdAsync(Guid campaignDiscountId)
        {
            return await _context.CampaignDiscounts
                .FirstOrDefaultAsync(c => c.Id == campaignDiscountId);
        }

        // Retrieves all campaign discounts from the database
        public async Task<IReadOnlyList<CampaignDiscount>> GetAllAsync()
        {
            return await _context.CampaignDiscounts
                .ToListAsync();
        }

        // Retrieves all active campaign discounts for a specific date
        public async Task<IReadOnlyList<CampaignDiscount>> GetActiveAsync(DateOnly date)
        {
            return await _context.CampaignDiscounts
                .Where(c => date >= c.StartDate && date <= c.EndDate)
                .ToListAsync();
        }

        // Creates and saves a new campaign discount
        public async Task CreateAsync(CampaignDiscount campaignDiscount)
        {
            await _context.CampaignDiscounts.AddAsync(campaignDiscount);
            await _context.SaveChangesAsync();
        }

        // Updates an existing campaign discount
        public async Task UpdateAsync(CampaignDiscount campaignDiscount)
        {
            _context.CampaignDiscounts.Update(campaignDiscount);
            await _context.SaveChangesAsync();
        }

        // Deletes a campaign discount if it exists
        public async Task DeleteAsync(Guid campaignDiscountId)
        {
            var existingCampaignDiscount = await GetByIdAsync(campaignDiscountId);

            if (existingCampaignDiscount != null)
            {
                _context.CampaignDiscounts.Remove(existingCampaignDiscount);
                await _context.SaveChangesAsync();
            }
        }
    }
}