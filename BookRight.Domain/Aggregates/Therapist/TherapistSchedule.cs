using BookRight.Domain.ValueObjects;
using Microsoft.IdentityModel.Tokens;
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
        public bool IsWorking { get; private set; } 

		private readonly List<TimeSlot> _blockedSlots = new(); //private field for at gemme timeslots til en therapist.
		public IReadOnlyCollection<TimeSlot> BlockedSlots => _blockedSlots.AsReadOnly(); //property for tilgang til timeslots.
        private TherapistSchedule() { } // for EF core

	  public static TherapistSchedule Create(
	    Guid therapistId,
		Guid clinicId,
		DateOnly date,
		bool isWorking)

		{
			if (therapistId == Guid.Empty)
				throw new ArgumentException("TherapistId må ikke være tom.");
			if (clinicId == Guid.Empty)
				throw new ArgumentException("ClinicId må ikke vare tom.");
			if (date < DateOnly.FromDateTime(DateTime.Today))
				throw new ArgumentException("Dato må ikke være i fortidden.");


			return new TherapistSchedule
			{
				Id = Guid.NewGuid(),
				TherapistId = therapistId,
				ClinicId = clinicId,
				Date = date,
				IsWorking = isWorking
			};

			
		}


		public bool IsAvailable(TimeSlot slot) 
		{
				return IsWorking && !_blockedSlots.Any(s => s.OverlapsWith(slot));				
		}
		public void BlockSlot(TimeSlot slot)
		{
				if (IsWorking == false)
				throw new ArgumentException("Behandler arbejder ikke dette tidspunkt");

			if (_blockedSlots.Any(s => s.OverlapsWith(slot)))
				throw new ArgumentException("Behandler har allerde behandling på dette tidspunkt");


			_blockedSlots.Add(slot);
				
		}
	}
}
