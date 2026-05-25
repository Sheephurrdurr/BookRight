using BookRight.Domain.ValueObjects;
using BookRight.Domain.Aggregates.CampaignDiscount;
using BookRight.Facade.DTOs.CreateCampaignDTOs;
using BookRight.Facade.Interfaces.DiscountUseCases;
using BookRight.UseCases.Interfaces;

namespace BookRight.UseCases.CampaignDiscountUseCases
{
    public class CreateCampaignDiscountUseCase : ICreateCampaignDiscountUseCase
    {
        private readonly ICampaignDiscountRepository _campaignDiscountRepository;

        public CreateCampaignDiscountUseCase(ICampaignDiscountRepository campaignDiscountRepository)
        {
            _campaignDiscountRepository = campaignDiscountRepository;
        }

        public async Task<CreateCampaignDiscountResponse> ExecuteAsync(CreateCampaignDiscountRequest request) 
        {
            var campaign = new CampaignDiscount(
                request.Name,
                request.DiscountPercent,
                new DateRange(request.StartDate, request.EndDate),
                request.TreatmentTypeIds
                );
            await _campaignDiscountRepository.CreateAsync(campaign);

            return new CreateCampaignDiscountResponse
            {
                Success = true,
                Message = "Kampagne oprettet!"
            };
        }
    }
}
