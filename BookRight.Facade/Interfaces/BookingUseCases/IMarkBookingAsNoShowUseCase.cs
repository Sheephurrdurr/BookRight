using BookRight.Facade.DTOs.MarkBookingAsNoShowDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.Facade.Interfaces.BookingUseCases
{
    public interface IMarkBookingAsNoShowUseCase
    {
        Task ExecuteAsync(MarkBookingAsNoShowRequest request);
    }
}
