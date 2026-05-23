namespace BookRight.Facade.DTOs.CustomerHistoryDTOs;

public record CustomerHistoryResponse(
    Guid CustomerId,
    string FullName,
    string Email,
    string Phone,
    string LoyaltyLevel,
    decimal TotalSpentLast12Months,
    IReadOnlyList<BookingHistoryItemResponse> Bookings
);

public record BookingHistoryItemResponse(
    Guid BookingId,
    DateTime StartTime,
    DateTime EndTime,
    decimal TotalPrice,
    string Status
);