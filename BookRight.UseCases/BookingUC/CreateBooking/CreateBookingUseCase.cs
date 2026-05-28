using BookRight.Domain.Aggregates.Booking;
using BookRight.Domain.Enums;
using BookRight.Domain.Errors;
using BookRight.Domain.Services;
using BookRight.Domain.Exceptions;
using BookRight.Domain.ValueObjects;
using BookRight.Facade.DTOs.CreateBookingDTOs;
using BookRight.Facade.Interfaces;
using BookRight.UseCases.Interfaces;

namespace BookRight.UseCases.BookingUC.CreateBooking
{
    public class CreateBookingUseCase : ICreateBookingUseCase
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IClinicRepository _clinicRepository;
        private readonly ITreatmentTypeRepository _treatmentTypeRepository;

        private readonly LoyaltyService _loyaltyService;
        private readonly DoubleBookingVerificationService _doubleBookingVerificationService;
        public CreateBookingUseCase(
            IBookingRepository bookingRepository,
            ICustomerRepository customerRepository,
            IClinicRepository clinicRepository,
            ITreatmentTypeRepository treatmentTypeRepository,

            LoyaltyService loyaltyService,
            DoubleBookingVerificationService doubleBookingVerificationService)
        {
            _bookingRepository = bookingRepository;
            _treatmentTypeRepository = treatmentTypeRepository; 
            _clinicRepository = clinicRepository;
            _customerRepository = customerRepository;

            _loyaltyService = loyaltyService;
            _doubleBookingVerificationService = doubleBookingVerificationService;
        }

        public async Task<CreateBookingResponse> ExecuteAsync (CreateBookingRequest request)
        {
            // Hent kunde via repository i infrastructure laget 
            var customer = await _customerRepository.GetByIdAsync(request.CustomerId);

            if (customer == null)
                throw new CustomerNotFoundException(request.CustomerId);

            // Check if the requested time slot is in the past 
            if (request.TimeSlot.StartTime < DateTime.Now)
            {
                throw new ArgumentException(
                    DomainErrorMessages.DateCannotBeBeforeToday,
                    nameof(request.TimeSlot.StartTime));
            }

            // Convert TimeSlot DTO til domain TimeSlot value object
            var timeSlot = new TimeSlot(request.TimeSlot.StartTime, request.TimeSlot.EndTime);

            var clinic = await _clinicRepository.GetByIdAsync(request.ClinicId);

            if (clinic == null)
                throw new ClinicNotFoundException(request.ClinicId);

            // Get relevant treatment types for the booking lines to check for group treatments and max participants
            var treatmentTypes = await _treatmentTypeRepository
                .GetByTherapistTreatmentTypeIdsAsync(
                    request.Lines.Select(l => l.TherapistTreatmentTypeId)
                );

            // Find first treatment type that is a group treatment (MaxParticipants > 1)
            var groupTreatmentType = treatmentTypes
                .FirstOrDefault(kvp => kvp.Value.MaxParticipants > 1);

            // Check current number of participants for the TimeSlot against max participants allowed
            if (groupTreatmentType.Key != Guid.Empty)
            {
                var currentCount = await _bookingRepository.CountParticipantsAsync(
                    groupTreatmentType.Key,
                    timeSlot
                    );

                // If currentCount is greater than or equal to maxParticipants, return response indicating booking is rejected due to full capacity
                if (currentCount >= groupTreatmentType.Value.MaxParticipants)
                {
                    return new CreateBookingResponse
                    {
                        Success = false,
                        Message = $"Der er ikke plads på holdet: ({currentCount}/{groupTreatmentType.Value.MaxParticipants}). Booking Afvist.)"
                    };
                }
            }

            // Get all completed bookings for the customer to determine loyalty level and potential discounts for the new booking
            var completedBookings = await _bookingRepository.GetAllBookingsByCustomerIdAsync(request.CustomerId);
            //Get all bookings by customer and therapist to check for overlap
            var allCustomerBooking = await _bookingRepository.GetByCustomerIdAsync(request.CustomerId);
            var allTherapistBooking = await _bookingRepository.GetByTherapistIdAsync(request.TherapistId);

            // Calculate the customer's loyalty level based on previous bookings  
            var loyaltyLevel = _loyaltyService.GetLoyaltyLevel(completedBookings, DateTime.Now);

            //Verifying both customer and therapist against double booking
            _doubleBookingVerificationService.CustomerBookingVerification(allCustomerBooking, timeSlot);
            _doubleBookingVerificationService.TherapistVerification(allTherapistBooking, timeSlot);

            // Create new Booking object using the Booking constructor
            var booking = new Booking(
                Guid.NewGuid(),
                request.CustomerId,
                request.TherapistId,
                request.ClinicId,
                timeSlot
            );


            request.Lines
                .Select(lineRequest => new BookingLine( // Create BookingLine object for each line in the request DTO
                    lineRequest.TherapistTreatmentTypeId, // Use ID from request DTO
                    new Money(lineRequest.BasePrice), // Create Money value object from base price in request DTO
                    0,
                    DiscountType.None 
                ))
                .ToList()
                .ForEach(booking.AddLine);

            // Create object of CreateBookingResponse DTO type to return as response
            await _bookingRepository.CreateAsync(booking);

            // Return success response
            return new CreateBookingResponse
            {
                Success = true,
                Message = "Booking oprettet!"
            };
        }
    }
}
