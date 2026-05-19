using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.Facade.DTOs.GetCustomerHealthNotesDTOs
{
    public record CustomerHealthNotesResponse(
       Guid CustomerId,
       string? HealthNotes
    );

}
