namespace BookRight.Domain.Aggregates.Clinic
{
    public class ClinicOpeningHour
    {
        public Guid Id { get; private set; }
        public Guid ClinicId { get; private set; }
        public string? Weekday { get; private set; }
        public TimeOnly OpensAt { get; private set; }
        public TimeOnly ClosesAt { get; private set; }

        public ClinicOpeningHour() { }

        public ClinicOpeningHour(Guid id, Guid clinicId, string weekDay, TimeOnly opensAt, TimeOnly closesAt) 
        {
            if (id == Guid.Empty)
                throw new ArgumentException(nameof(id));

            if (clinicId == Guid.Empty)
                throw new ArgumentException(nameof(clinicId));

            if ()
                throw new ArgumentNullException(nameof(opensAt));
        }
    }
}
