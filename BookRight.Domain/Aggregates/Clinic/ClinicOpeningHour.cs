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
                throw new ArgumentException("ClinicId må ikke være tom.", nameof(clinicId));

            if (openTime >= closeTime)
                throw new ArgumentException("Åbningstid skal være før lukketid.");

            Id = Guid.NewGuid();
            ClinicId = clinicId;
            DayOfWeek = dayOfWeek;
            OpenTime = openTime;
            CloseTime = closeTime;
        }
    }
}