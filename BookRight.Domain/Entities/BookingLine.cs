using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.Domain.Entities
{
    public class BookingLine
    {
        public Guid Id { get; private set; }
        public Guid TherapistTreatmentTypeId { get; private set; }
        public decimal BasePrice { get; private set; }
    }
}
