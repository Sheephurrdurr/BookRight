using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.Facade.DTOs.GetAvailableTherapistsDTOs
{
    public class GetAvailableTherapistsRequest
    {
        public Guid ClinicId { get; set; }
        public DateOnly Date { get; set; }
    }
}
