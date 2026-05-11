using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.Domain.Aggregates.Therapist
{
	public class TherapistSchedule
	{
	   public Guid Id { get; private set; }
	   public Guid TherapistId { get; private set; }
	   public Guid ClinicId { get; private set; }
	   public DateOnly Date { get; private set; }

	   private TherapistSchedule () { } // for EF core

	   public static TherapistSchedule Create(
	   Guid therapistId,
	   Guid clinicId,
	   DateOnly date
	   )

	   {
			if (therapistId == Guid.Empty)
				throw new ArgumentException("TherapistId må ikke være tom.");
			if (clinicId == Guid.Empty)
				throw new ArgumentException("ClinicId må ikke være tom.");
				// sikrer at en behandler ikke kan planlægges på en dato der allerede er passeret
			if (date < DateOnly.FromDateTime(DateTime.Today))
				throw new ArgumentException("Dato må ikke være i fortiden.");

			return new TherapistSchedule
			{
				Id = Guid.NewGuid(),
				TherapistId = therapistId,
				ClinicId = clinicId,
				Date = date
			};
				
		}
	}
}
