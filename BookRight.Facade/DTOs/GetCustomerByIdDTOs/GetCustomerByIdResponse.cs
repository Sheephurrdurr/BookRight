
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

    // DTO returneres kun af use cases der har brug for den, så følsom brugerdata ikke bliver exponeret.
    public record CustomerHealthNoteResponse(
        Guid CustomerId,
        string? Healthnote
    );
}
