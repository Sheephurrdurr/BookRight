using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.Facade.DTOs.GetAllTreatmentTypeDTOs
{
    public record GetAllTreatmentTypeResponse(
        Guid Id,
        string Name,
        int DurationMinutes,
        int MaxParticipants
    );
}
