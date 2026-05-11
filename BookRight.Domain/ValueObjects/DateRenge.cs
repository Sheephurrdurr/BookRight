using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.Domain.ValueObjects
{
	public class DateRenge
	{
	  public DateTime Start { get; private set; }
	  public DateTime End { get; private set; }


	  private DateRenge()  { } // Til EF core kræver en parameterløs constructor

	  public static DateRenge Create(DateTime start, DateTime end)
	  {
			if (start >= end)
				throw new ArgumentException("Start skal være før end.");


		 return new DateRenge { Start = start,  End = end };
	  }

	  public bool Overlaps(DateRenge other)
	  {
			return Start < other.End && End > other.Start;
	  }

	  public bool Contains(DateTime date)
	  {
			return date >= Start && date <= End;
	  }
	}
}
