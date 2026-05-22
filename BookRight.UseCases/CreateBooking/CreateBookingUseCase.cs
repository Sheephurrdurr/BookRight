using BookRight.Domain.Aggregates.Booking;
using BookRight.Domain.Enums;
using BookRight.Domain.Errors;
using BookRight.Domain.Services;
using BookRight.Domain.Exceptions;
using BookRight.Domain.ValueObjects;
using BookRight.Facade.DTOs.CreateBookingDTOs;
using BookRight.Facade.Interfaces;
using BookRight.UseCases.Interfaces;

namespace BookRight.UseCases.CreateBooking
{
    public class CreateBookingUseCase : ICreateBookingUseCase
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IClinicRepository _clinicRepository;
        private readonly ITreatmentTypeRepository _treatmentTypeRepository;

        private readonly LoyaltyService _loyaltyService;
        public CreateBookingUseCase(
            IBookingRepository bookingRepository,
            ICustomerRepository customerRepository,
            IClinicRepository clinicRepository,
            ITreatmentTypeRepository treatmentTypeRepository,

            LoyaltyService loyaltyService)
        {
            _bookingRepository = bookingRepository;
            _treatmentTypeRepository = treatmentTypeRepository; 
            _clinicRepository = clinicRepository;
            _customerRepository = customerRepository;

            _loyaltyService = loyaltyService;
        }

        public async Task<CreateBookingResponse> ExecuteAsync (CreateBookingRequest request)
        {
            // Hent kunde via repository i infrastructure laget 
            var customer = await _customerRepository.GetByIdAsync(request.CustomerId);

            if (customer == null)
                throw new CustomerNotFoundException(request.CustomerId);

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

            // Check if its a group treatment and if so, check current number of participants for the time slot against max participants allowed
            if (groupTreatmentType.Key != Guid.Empty)
            {
                var currentCount = await _bookingRepository.CountParticipantsAsync(
                    groupTreatmentType.Key,
                    timeSlot
                    );

                // If current count is greater than or equal to max participants, return response indicating booking is rejected due to full capacity
                if (currentCount >= groupTreatmentType.Value.MaxParticipants)
                {
                    return new CreateBookingResponse
                    {
                        Success = false,
                        Message = $"Holdet er fuldt ({currentCount}/{groupTreatmentType.Value.MaxParticipants}). Booking Afvist.)"
                    };
                }
            }

            // Use Repository to get all completed bookings for the customer to determine loyalty level and potential discounts for the new booking
            var completedBookings = await _bookingRepository.GetAllBookingsByCustomerIdAsync(request.CustomerId);

            // Use LoyaltyService to calculate the customer's loyalty level based on previous bookings  
            var loyaltyLevel = _loyaltyService.GetLoyaltyLevel(completedBookings, DateTime.Now);

            // Check if the requested time slot is in the past 
            if (request.TimeSlot.StartTime < DateTime.Now)
            {
                throw new ArgumentException(
                    DomainErrorMessages.DateCannotBeBeforeToday,
                    nameof(request.TimeSlot.StartTime));
            }


            // Create the new Booking object using the domain model
            var booking = new Booking(
                Guid.NewGuid(),
                request.CustomerId,
                request.ClinicId,
                timeSlot
            );

            request.Lines
                .Select(lineRequest => new BookingLine( // Opret booking line for hver linje i request DTO
                    lineRequest.TherapistTreatmentTypeId, // Brug ID fra request DTO
                    new Money(lineRequest.BasePrice), // Opret Money value object fra base price i request DTO
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
                Success = true,
                Message = "Booking oprettet med ID: " + booking.Id
            };
        }
    }
}
