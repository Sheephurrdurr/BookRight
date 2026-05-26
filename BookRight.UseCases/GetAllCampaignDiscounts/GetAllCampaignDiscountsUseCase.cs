using BookRight.Facade.DTOs.GetAllCampaignDiscountsDTOs;
using BookRight.Facade.Interfaces.DiscountUseCases;
using BookRight.UseCases.Interfaces;

namespace BookRight.UseCases.GetAllCampaignDiscounts
{
    public class GetAllCampaignDiscountsUseCase : IGetAllCampaignDiscountsUseCase
    {
        private readonly ICampaignDiscountRepository _repository;

        public GetAllCampaignDiscountsUseCase(ICampaignDiscountRepository repository)
        {
            _repository = repository;
        }

        public async Task<IReadOnlyList<GetAllCampaignDiscountsResponse>> ExecuteAsync()
        {
            var campaigns  = await _repository.GetAllAsync();

            return campaigns.Select(t => new GetAllCampaignDiscountsResponse
            {
                Id = t.Id,
                Name = t.Name,
                DiscountPercentage = t.DiscountPercent,
                StartDate = t.DateRange.StartDate,
                EndDate = t.DateRange.EndDate,
                AppliesToTreatmentTypeIds = t.AppliesToTreatmentTypeIds
            })
            .ToList();
        }

    }
}