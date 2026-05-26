using BookRight.Domain.Errors;
using System;

namespace BookRight.Domain.Aggregates.Clinic
{
    public class ClinicOpeningHour
    {
        public Guid Id { get; private set; }
        public Guid ClinicId { get; private set; }
        public DayOfWeek DayOfWeek { get; private set; }
        public TimeOnly OpenTime { get; private set; }
        public TimeOnly CloseTime { get; private set; }


        // Privat constructor kræves af EF Core
        private ClinicOpeningHour() { }


        // Constructor bruges til at oprette åbningstid for en bestemt klinik og ugedag
        public ClinicOpeningHour(Guid clinicId, DayOfWeek dayOfWeek, TimeOnly openTime, TimeOnly closeTime)
        {
            if (clinicId == Guid.Empty)
                throw new ArgumentException(nameof(clinicId));

            if (openTime >= closeTime)
                throw new ArgumentException(
                    DomainErrorMessages.OpeningTimeMustBeBeforeClosingTime,
                    nameof(openTime));

            Id = Guid.NewGuid();
            ClinicId = clinicId;
            DayOfWeek = dayOfWeek;
            OpenTime = openTime;
            CloseTime = closeTime;
        }
    }
}