using BookRight.Domain.Errors;
using BookRight.Domain.Exceptions;
using BookRight.Domain.ValueObjects;

namespace BookRight.Domain.Aggregates.AddOn
{
    public class AddOn
    {
        public Guid Id { get; private set; }

        public string Name { get; private set; } = null!; //Name IS NOT nullable. It's a promise to the EF constructor, that name is set later. If not it results in a warning. // Ex. "Weekendtillæg"

        public decimal Percentage { get; private set; } //Ex. 15 %

        private AddOn() { }

        public AddOn(string name, decimal percentage)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException(
                    DomainErrorMessages.NameCannotBeEmpty,
                    nameof(name));

            if (percentage < 0 || percentage > 100)
                throw new InvalidAddOnPercentageException();

            Id = Guid.NewGuid();
            Name = name;
            Percentage = percentage;
        }


        /*Calculates Surchrage in DKK.
        Ex. BasePrice = 400 kr
        Percentage = 15
        Result = 60 kr*/
        public Money CalculateAmount(Money basePrice) 
        {
            decimal multiplier = Percentage / 100m;

            return basePrice * multiplier;
        }
    }
}