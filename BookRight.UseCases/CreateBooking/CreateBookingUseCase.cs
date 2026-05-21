using BookRight.Domain.Aggregates.Booking;
using BookRight.Domain.Enums;
using BookRight.Domain.Exceptions;
using BookRight.Domain.ValueObjects;
using BookRight.Facade.DTOs.CreateBookingDTOs;
using BookRight.Facade.DTOs.ValueObjectDTOs;
using BookRight.Facade.Interfaces;
using BookRight.UseCases.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookRight.UseCases.CreateBooking
{
    public class CreateBookingUseCase : ICreateBookingUseCase
    {
        private readonly Interfaces.IBookingRepository _bookingRepository;
        private readonly Interfaces.ICustomerRepository _customerRepository;
        private readonly IClinicRepository _clinicRepository;
        public CreateBookingUseCase(
            Interfaces.IBookingRepository bookingRepository,
            Interfaces.ICustomerRepository customerRepository,
            IClinicRepository clinicRepository)
        {
            _bookingRepository = bookingRepository;

            _clinicRepository = clinicRepository;
            _customerRepository = customerRepository;
        }

        public async Task<CreateBookingResponse> ExecuteAsync (CreateBookingRequest request)
        {
            // Valider at kunde findes
            var customer = await _customerRepository.GetByIdAsync(request.CustomerId);

            if (customer == null)
                throw new CustomerNotFoundException(request.CustomerId);

            var clinic = await _clinicRepository.GetByIdAsync(request.ClinicId);

            if (clinic == null)
                throw new ClinicNotFoundException(request.ClinicId);

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
