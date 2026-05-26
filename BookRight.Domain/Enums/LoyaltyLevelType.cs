using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.Domain.Enums
{
	public enum LoyaltyLevelType
	{
	 None, // under 3,000 kr. ingen rabat.
	 Bronze, // mellem 3,000 kr. og 10,000 kr. 5% rabat.
	 Silver, // mellem 10,001 kr. og 25,000 kr. 10% rabat.
	 Gold, // over 25,000 kr. 15% rabat.
	}
}
