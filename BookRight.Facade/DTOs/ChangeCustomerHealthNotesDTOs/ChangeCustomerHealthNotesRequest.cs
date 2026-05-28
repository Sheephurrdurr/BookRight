namespace BookRight.Facade.DTOs.ChangeCustomerHealthNotesDTOs
{
    public sealed record ChangeCustomerHealthNotesRequest(
        Guid CustomerId,
        string? HealthNotes
    );
}
