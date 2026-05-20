using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.Facade.DTOs.GetAvailableTherapistsDTOs
{
    public record GetAvailableTherapistsRequest
    {
        public Guid ClinicId { get; set; }
        public DateOnly Date { get; set; }
    }
}
