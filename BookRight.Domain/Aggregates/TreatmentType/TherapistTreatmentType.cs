using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.Domain.Aggregates.TreatmentType
{
    public class TherapistTreatmentType
    {
        public Guid id { get; private set; }
        public Guid therapistId { get; private set; }
        public Guid treatmentTypeId { get; private set; }
        public decimal basePrice { get; private set; }
       
        public TherapistTreatmentType () { }


    }
}
