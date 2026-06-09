namespace BookRight.Domain.Exceptions
{
    public class CampaignDiscountNotValidException : DomainException
    {
        // DateRange bruger en eksklusiv slutdato.
        // Derfor trækkes én dag fra EndDate, så brugeren ser den sidste gyldige kampagnedag.
        public CampaignDiscountNotValidException(
         string campaignName,
         DateOnly startDate)
         : base(
             $"Denne kampagne kan ikke bruges udenfor kampagnens periode. " +
             $"{campaignName} gælder kun den {startDate:dd/MM/yyyy}.")
        {
        }
    }
}