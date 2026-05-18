using System;
using System.Collections.Generic;
using System.Text;

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
			    throw new ArgumentException("Street cannot be empty,", nameof(street));
			if (string.IsNullOrWhiteSpace(city))
				throw new ArgumentException("City cannot be empty.", nameof(city));
			if (string.IsNullOrWhiteSpace(postalCode))
				throw new ArgumentException("Postalcode cannot be empty.");

			Street = street;
			City = city;
			PostalCode = postalCode;
		}
	}
}
// public  -> Kan bruges fra andre layers/projekter.
// sealed  -> Kan ikke nedarves.
// record  -> Sammenlignes på værdier i stedet for reference.