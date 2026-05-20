using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.Facade.DTOs.GetAvailableTherapistsDTOs
{
    public record AvailableTherapistResponse
    {
        public Guid TherapistId { get; set; }
        public string Name { get; set; } // Så UI har noget tekst at vise i <select>
    }
}
