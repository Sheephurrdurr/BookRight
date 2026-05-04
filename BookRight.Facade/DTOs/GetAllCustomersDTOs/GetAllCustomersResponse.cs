namespace BookRight.Facade.DTOs.GetAllCustomersDTOs
{
    public record GetAllCustomersResponse(
        Guid Id,
        string FirstName,
        string LastName,
        string Email,
        string Phone
    );
}
