using BookRight.Domain.Errors;

namespace BookRight.Domain.ValueObjects
{
	public sealed record Address
	{
	  public string Street { get; private set; }
	  public string City { get; private set; }
	  public string PostalCode { get; private set; }

	  public Address(string street, string city, string postalCode)
	  {
            if (string.IsNullOrWhiteSpace(street))
                throw new ArgumentException(
                    DomainErrorMessages.StreetCannotBeEmpty,
                    nameof(street));

            if (string.IsNullOrWhiteSpace(city))
                throw new ArgumentException(
                    DomainErrorMessages.CityCannotBeEmpty,
                    nameof(city));

            if (string.IsNullOrWhiteSpace(postalCode))
                throw new ArgumentException(
                    DomainErrorMessages.PostalCodeCannotBeEmpty,
                    nameof(postalCode));

            Street = street;
			City = city;
			PostalCode = postalCode;
		}
	}
}
// public  -> Kan bruges fra andre layers/projekter.
// sealed  -> Kan ikke nedarves.
// record  -> Sammenlignes på værdier i stedet for reference.