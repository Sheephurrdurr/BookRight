using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.Facade.DTOs.DeleteTreatmentTypeDTOs
{
    public record DeleteTreatmentTypeRequest
    {
        public Guid Id { get; init; }
    }
}
