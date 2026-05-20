using BookRight.Domain.Aggregates.Booking;
using BookRight.Domain.Enums;
using BookRight.Domain.Services;
using BookRight.Domain.ValueObjects;
using BookRight.Facade.DTOs.CreateBookingDTOs;
using BookRight.Facade.Interfaces;
using BookRight.UseCases.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookRight.UseCases.CreateBooking
{
    public class CreateBookingUseCase : ICreateBookingUseCase
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IClinicRepository _clinicRepository;
        private readonly LoyaltyService _loyaltyService;
        public CreateBookingUseCase(
            IBookingRepository bookingRepository,
            ICustomerRepository customerRepository,
            IClinicRepository clinicRepository,
            LoyaltyService loyaltyService)
        {
            _bookingRepository = bookingRepository;

            _clinicRepository = clinicRepository;
            _customerRepository = customerRepository;

            _loyaltyService = loyaltyService;
        }

        public async Task<CreateBookingResponse> ExecuteAsync (CreateBookingRequest request)
        {
            // Hent kunde via repository i infrastructure laget 
            var customer = await _customerRepository.GetByIdAsync(request.CustomerId);

            if (customer == null)
                throw new KeyNotFoundException($"Customer with Id: {request.CustomerId} findes ikke.");
                        
            var clinic = await _clinicRepository.GetByIdAsync(request.ClinicId);

            if (clinic == null)
                throw new KeyNotFoundException($"Clinic with Id: {request.ClinicId} does not exist.");

            // Brug repository metode til at hente alle tidligere bookinger for kunden
            var completedBookings = await _bookingRepository.GetAllBookingsByCustomerIdAsync(request.CustomerId);

            // Brug LoyaltyService til at beregne kundens loyalitetsniveau baseret på tidligere bookinger
            var loyaltyLevel = _loyaltyService.GetLoyaltyLevel(completedBookings, DateTime.Now);

            var timeSlot = new TimeSlot(request.TimeSlot.StartTime, request.TimeSlot.EndTime);

            // Opret booking via domain factory
            var booking = new Booking(
                Guid.NewGuid(),
                request.CustomerId,
                request.ClinicId,
                timeSlot
            );

            request.Lines
                .Select(lineRequest => new BookingLine(
                lineRequest.TherapistTreatmentTypeId,
                new Money(lineRequest.BasePrice),
                0,
                DiscountType.None
                ))
                .ToList()
                .ForEach(booking.AddLine);

            // Gem i databasen
            await _bookingRepository.CreateAsync(booking);

            // Returener response DTO
            return new CreateBookingResponse
            {
                Id = booking.Id
            };
        }
    }
}
