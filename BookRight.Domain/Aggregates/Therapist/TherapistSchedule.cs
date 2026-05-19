using BookRight.Domain.Errors;
using BookRight.Domain.Exceptions;
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
                throw new ArgumentException(nameof(therapistId)); //Only for developers. Errormessages is only for UI.

            if (clinicId == Guid.Empty)
                throw new ArgumentException(nameof(clinicId));

            if (date < DateOnly.FromDateTime(DateTime.Today))
                throw new ArgumentException(
                    DomainErrorMessages.DateCannotBeBeforeToday,
                    nameof(date));

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
            if (slot is null)
                throw new ArgumentNullException(nameof(slot)); //No errormessage, only for developers

            if (IsWorking == false)
                return false;

            foreach (TimeSlot t in _blockedSlots)
                if (t.OverlapsWith(slot))
                    return false;

            return true;
        }

        public void BlockSlot(TimeSlot slot)
        {
            if (slot is null)
                throw new ArgumentNullException(nameof(slot));

            if (IsWorking == false)
                 throw new TherapistIsNotAvailableException(); //Custom exception

            foreach (TimeSlot t in _blockedSlots)
                if (t.OverlapsWith(slot))
                    throw new TherapistAlreadyHasBookingException(); //Custom exception

            _blockedSlots.Add(slot);
        }
    }
}
