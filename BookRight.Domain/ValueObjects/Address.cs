using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.Domain.ValueObjects
{
	public record Address
	{
	  public string Street { get; private set; }
	  public string City { get; private set; }
	  public string PostalCode { get; private set; }
	  public string Country { get; private set; }

	  public Address(string street, string city, string postalCode, string country)
	  {
			if (string.IsNullOrWhiteSpace(street))
			    throw new ArgumentException("Street cannot be empty,", nameof(street));
			if (string.IsNullOrWhiteSpace(city))
				throw new ArgumentException("City cannot be empty.", nameof(city));
			if (string.IsNullOrWhiteSpace(PostalCode))
				throw new ArgumentException("Postalcode cannot be empty.");
			if (string.IsNullOrWhiteSpace(Country))
				throw new ArgumentException("Country cannot be empty.", nameof(country));

			Street = street;
			City = city;
			PostalCode = postalCode;
			Country = country;
		}
	}
}
