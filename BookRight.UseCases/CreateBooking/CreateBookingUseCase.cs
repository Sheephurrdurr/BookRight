using BookRight.Domain.Entities;
using BookRight.Domain.Enums;
using BookRight.Facade.DTOs;
using BookRight.Facade.DTOs.CreateBookingDTOs;
using BookRight.Facade.Interfaces;
using BookRight.UseCases.Interfaces;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace BookRight.UseCases.CreateBooking
{
    public class CreateBookingUseCase : ICreateBookingUseCase
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly ICustomerRepository _customerRepository;

        public CreateBookingUseCase(
            IBookingRepository bookingRepository,
            ICustomerRepository customerRepository)
        {
            _bookingRepository = bookingRepository;
            _customerRepository = customerRepository;
        }

        public async Task<CreateBookingResponse> ExecuteAsync (CreateBookingRequest request)
        {
            // 1. valider at kunde findes
            var customer = await _customerRepository.GetByIdAsync(request.CustomerId);
            if (customer == null)
                throw new Exception("Customer not found");

            // 2. opret booking via domain factory
            var booking = Booking.CreateBooking(
                Guid.NewGuid(),
                request.CustomerId,
                request.Date,
                request.TimeSlot,
                request.Duration,
                BookingStatus.Completed
                );

            // 3. sæt Fremmed nøgle
           /* Booking.CustomerId = request.CustomerId;*/

            // 4. Gem i databasen
            await _bookingRepository.CreateAsync(booking);

            // 5. Returener response DTO
            return new CreateBookingResponse
            {
                BookingId = booking.BookingId,
                CustomerId = booking.CustomerId,
                Date = booking.Date,
                TimeSlot = booking.TimeSlot,
                Duration = booking.Duration,
                Status = booking.Status.ToString()
            };
        }
    }
}
