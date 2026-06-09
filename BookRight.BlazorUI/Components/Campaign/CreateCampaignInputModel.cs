namespace BookRight.BlazorUI.Components.Campaign
{
    public class CreateCampaignInputModel
    {
        public string Name { get; set; } = string.Empty;
        public decimal DiscountPercent { get; set; }
        public DateOnly StartDate { get; set; } = DateOnly.FromDateTime(DateTime.Today);
        public DateOnly EndDate { get; set; } = DateOnly.FromDateTime(DateTime.Today);
    }
}
