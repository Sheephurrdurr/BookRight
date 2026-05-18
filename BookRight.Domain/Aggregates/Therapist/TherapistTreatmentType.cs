
namespace BookRight.Domain.Aggregates.Therapist
{
    public class TherapistTreatmentType //Ingen configurations endnu...
    {
        public Guid Id { get; private set; }
        public Guid TherapistId { get; private set; }
        public Guid TreatmentTypeId { get; private set; }
        public decimal BasePrice { get; private set; }
       
        private TherapistTreatmentType () { }

        public TherapistTreatmentType(Guid therapistId, Guid treatmentTypeId, decimal basePrice)
        {
            if (therapistId == Guid.Empty)
                throw new ArgumentException("Therapist ID må ikke være tomt.", nameof(therapistId));

            if (treatmentTypeId == Guid.Empty)
                throw new ArgumentException("Treatment Type ID må ikke være tomt.", nameof(treatmentTypeId));

            if (basePrice <= 0)
                throw new ArgumentException("Prisen skal være højere end nul.", nameof(basePrice));

            Id = Guid.NewGuid();
            TherapistId = therapistId;
            TreatmentTypeId = treatmentTypeId;
            BasePrice = basePrice;
        }
    }
}
