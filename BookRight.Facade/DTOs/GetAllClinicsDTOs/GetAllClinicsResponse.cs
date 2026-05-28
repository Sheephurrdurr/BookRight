using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.Facade.DTOs.GetAllClinicsDTOs
{
    public sealed record GetAllClinicsResponse(Guid Id, string Name);
}
