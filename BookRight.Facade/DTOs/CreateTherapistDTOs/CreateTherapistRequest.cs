namespace BookRight.Facade.DTOs.CreateTherapistDTOs
{
    public record CreateTherapistRequest
    {
       public string FirstName { get; set; }
       public string LastName { get; set; }
       public string Email { get; set; }
       public string Specialization { get; set; }
       public Guid ClinicId { get; set; }
    }
       
}