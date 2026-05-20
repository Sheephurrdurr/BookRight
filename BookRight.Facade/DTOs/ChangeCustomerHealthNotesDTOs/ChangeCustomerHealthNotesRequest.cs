namespace BookRight.Facade.DTOs.ChangeCustomerHealthNotesDTOs
{
    public record ChangeCustomerHealthNotesRequest(
        Guid CustomerId,
        string? HealthNotes
    );
}
