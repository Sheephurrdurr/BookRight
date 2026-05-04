namespace BookRight.Facade.DTOs.GetAllTherapistsDTOs
{
    public record GetAllTherapistsResponse(
         Guid Id,
         string FirstName,
         string LastName,
         string Email,
         string Specialization
     );
}
