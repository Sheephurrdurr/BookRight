using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.Domain.Aggregates.TreatmentType
{
    public class TherapistTreatmentType //Ingen configurations endnu...
    {
        public Guid id { get; private set; }
        public Guid therapistId { get; private set; }
        public Guid treatmentTypeId { get; private set; }
        public decimal basePrice { get; private set; }
       
        public TherapistTreatmentType () { }

        public TherapistTreatmentType(Guid therapistId, Guid treatmentTypeId, decimal basePrice)
        {
            if (therapistId == Guid.Empty)
                throw new ArgumentException("Therapist ID må ikke være tomt.", nameof(therapistId));

            if (treatmentTypeId == Guid.Empty)
                throw new ArgumentException("Treatment Type ID må ikke være tomt.", nameof(treatmentTypeId));

            if (basePrice <= 0)
                throw new ArgumentException("Prisen skal være højere end nul.", nameof(basePrice));

            id = Guid.NewGuid();
            this.therapistId = therapistId;
            this.treatmentTypeId = treatmentTypeId;
            this.basePrice = basePrice;
        }
    }
}
