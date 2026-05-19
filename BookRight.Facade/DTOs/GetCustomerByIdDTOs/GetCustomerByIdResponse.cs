namespace BookRight.Facade.DTOs.GetCustomerByIdDTOs
{
    public record GetCustomerByIdResponse(
        Guid Id,
        string FirstName,
        string LastName,
        string Email,
        string Phone,
        DateOnly DateOfBirth,
        Guid? PreferredTherapistId
    );
}
