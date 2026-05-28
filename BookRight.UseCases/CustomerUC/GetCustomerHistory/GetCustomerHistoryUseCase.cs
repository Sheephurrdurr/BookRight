using BookRight.Domain.Enums;
using BookRight.Facade.DTOs.CustomerHistoryDTOs;
using BookRight.Facade.Interfaces.CustomerUseCases;
using BookRight.UseCases.Interfaces;

namespace BookRight.UseCases.CustomerUC.GetCustomerHistory;

public class GetCustomerHistoryUseCase : IGetCustomerHistoryUseCase
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IBookingRepository _bookingRepository;

    public GetCustomerHistoryUseCase(
        ICustomerRepository customerRepository,
        IBookingRepository bookingRepository)
    {
        _customerRepository = customerRepository;
        _bookingRepository = bookingRepository;
    }

    public async Task<IReadOnlyList<CustomerHistoryResponse>> ExecuteAsync(string query)//Retrieves customer, their booking history and loyalty lvl by email, phone number, first name or last name
    {
        if (string.IsNullOrWhiteSpace(query))
            return new List<CustomerHistoryResponse>();

        var customers = await _customerRepository.SearchAsync(query);

        var responses = new List<CustomerHistoryResponse>();

        foreach (var customer in customers)
        {
            var bookings = await _bookingRepository.GetByCustomerIdAsync(customer.Id);

            var twelveMonthsAgo = DateTime.Now.AddMonths(-12);

            var completedBookingsLast12Months = bookings //Only completed bookings within last 12 months used when calculating loyaltylvl.
                .Where(b => b.Status == BookingStatus.Completed &&
                            b.TimeSlot.StartTime >= twelveMonthsAgo);

            var totalSpent = completedBookingsLast12Months
                .Sum(b => b.GetTotalPrice().Value);

            var loyaltyLevel = GetLoyaltyLevel(totalSpent);

            var history = bookings //All bookings included cancelled bookings and no-shows
                .OrderByDescending(b => b.TimeSlot.StartTime)
                .Select(b => new BookingHistoryItemResponse(
                    b.Id,
                    b.TimeSlot.StartTime,
                    b.TimeSlot.EndTime,
                    b.GetTotalPrice().Value,
                    b.Status.ToString()
                ))
                .ToList();

            responses.Add(new CustomerHistoryResponse(
                customer.Id,
                $"{customer.Name.FirstName} {customer.Name.LastName}",
                customer.Email.Value,
                customer.Phone.Value,
                loyaltyLevel,
                totalSpent,
                history
            ));
        }

        return responses;
    }

    private static string GetLoyaltyLevel(decimal totalSpent) //Determines loyaltylvl based in customers total spending from completed bookings within last 12 months
    {
        if (totalSpent > 25000) return "Guld";
        if (totalSpent >= 10001) return "Sølv";
        if (totalSpent >= 3000) return "Bronze";

        return "Ingen";
    }
}