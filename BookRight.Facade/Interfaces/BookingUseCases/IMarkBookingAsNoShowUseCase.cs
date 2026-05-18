using BookRight.Facade.DTOs.MarkBookingAsNoShowDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.Facade.Interfaces.BookingUseCases
{
    public interface IMarkBookingAsNoShowUseCase // Use case interface for marking a booking as a no-show
    {
        Task ExecuteAsync(MarkBookingAsNoShowRequest request); // Method to execute the use case, taking a request object as a parameter
    }
}
