using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.Facade.DTOs.CreateTherapistDTOs
{
    public record CreateTherapistRequest(string FirstName, string LastName, string Email, string Specialization);
}

