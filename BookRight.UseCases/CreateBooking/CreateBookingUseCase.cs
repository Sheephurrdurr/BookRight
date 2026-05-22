using BookRight.Domain.Aggregates.Booking;
using BookRight.Domain.Aggregates.TreatmentType;
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

            var clinic = await _clinicRepository.GetByIdAsync(request.ClinicId);

            if (clinic == null)
                throw new ClinicNotFoundException(request.ClinicId);

            var treatmentTypes = await _treatmentTypeRepository
                .GetByTherapistTreatmentTypeIdsAsync(
                    request.Lines.Select(l => l.TherapistTreatmentTypeId)
                );

            var groupTreatmentType = treatmentTypes.FirstOrDefault(t => t.MaxParticipants > 1);
            
            // Check for GroupTreatmentTypes. If none are found, skip this check.
            if (groupTreatmentType == null)
            {
                return;
            }

            // Brug repository metode til at hente alle tidligere bookinger for kunden
            var completedBookings = await _bookingRepository.GetAllBookingsByCustomerIdAsync(request.CustomerId);

            // Brug LoyaltyService til at beregne kundens loyalitetsniveau baseret på tidligere bookinger
            var loyaltyLevel = _loyaltyService.GetLoyaltyLevel(completedBookings, DateTime.Now);

            // Valider at bookingens starttidspunkt ikke er i fortiden
            if (request.TimeSlot.StartTime < DateTime.Now)
            {
                throw new ArgumentException(
                    DomainErrorMessages.DateCannotBeBeforeToday,
                    nameof(request.TimeSlot.StartTime));
            }
            var timeSlot = new TimeSlot(request.TimeSlot.StartTime, request.TimeSlot.EndTime);

            // Opret booking via domain factory
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
                Id = booking.Id
            };
        }
    }
}
