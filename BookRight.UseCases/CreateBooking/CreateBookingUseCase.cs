using BookRight.Domain.Aggregates.Booking;
using BookRight.Domain.Aggregates.TherapistAggregate;
using BookRight.Domain.Enums;
using BookRight.Domain.Errors;
using BookRight.Domain.Exceptions;
using BookRight.Domain.Services;
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
        private readonly ICampaignDiscountRepository _campaignDiscountRepository;
        private readonly ITherapistRepository _therapistRepository;

        private readonly LoyaltyService _loyaltyService;
        private readonly DoubleBookingVerificationService _doubleBookingVerificationService;
        private readonly PriceCalculatorService _priceCalculatorService;
        public CreateBookingUseCase(
            IBookingRepository bookingRepository,
            ICustomerRepository customerRepository,
            IClinicRepository clinicRepository,
            ITreatmentTypeRepository treatmentTypeRepository,
            ICampaignDiscountRepository campaignDiscountRepository,
            ITherapistRepository therapistRepository,

            LoyaltyService loyaltyService,
            DoubleBookingVerificationService doubleBookingVerificationService, 
            PriceCalculatorService priceCalculatorService)
        {
            _bookingRepository = bookingRepository;
            _treatmentTypeRepository = treatmentTypeRepository; 
            _clinicRepository = clinicRepository;
            _customerRepository = customerRepository;
            _campaignDiscountRepository = campaignDiscountRepository;
            _therapistRepository = therapistRepository;

            _loyaltyService = loyaltyService;
            _doubleBookingVerificationService = doubleBookingVerificationService;
            _priceCalculatorService = priceCalculatorService;
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

            // Get relevant treatment types for the booking lines 
            var treatmentTypes = await _treatmentTypeRepository
                .GetByTherapistTreatmentTypeIdsAsync(
                    request.Lines.Select(l => l.TherapistTreatmentTypeId)
                );

            // Handles Bookings with more than one treatment in them (BookingLine). 
            // If there are more than 1 treatment in the booking -
            if (request.Lines.Count() > 1)
            {
                // Find treatmentTypes that cant be combined
                var nonCombinableType = treatmentTypes.Values
                    .FirstOrDefault(t => !t.CanBeCombined);

                if (nonCombinableType != null)
                {
                    throw new ArgumentException(
                        DomainErrorMessages
                        .TreatmentTypeCannotBeCombinedWith(nonCombinableType.Name));
                }
            }
            if (clinic == null)
                throw new ClinicNotFoundException(request.ClinicId);

           

            var therapist = await _therapistRepository.GetByIdAsync(request.TherapistId);
            if (therapist == null)
                throw new TherapistNotFoundException(request.TherapistId);
            //Checks if the therapist is connected to the chosen clinic.
            if (therapist.ClinicId != clinic.Id)
            {
                throw new InvalidOperationException(
                    "Behandleren er ikke tilknyttet den valgte klinik.");
            }

            // Get all completed bookings for the customer to determine loyalty level and potential discounts for the new booking
            var completedBookings = await _bookingRepository.GetAllBookingsByCustomerIdAsync(request.CustomerId);
            //Get all bookings by customer and therapist to check for overlap
            var allCustomerBooking = await _bookingRepository.GetByCustomerIdAsync(request.CustomerId);
            var allTherapistBooking = await _bookingRepository.GetByTherapistIdAsync(request.TherapistId);

            var campaignDiscount = request.CampaignDiscountId.HasValue
                ? await _campaignDiscountRepository.GetByIdAsync(request.CampaignDiscountId.Value)
                : null;


            // Check if the requested time slot is in the past 
            if (request.StartTime < DateTime.Now)
            {
                throw new ArgumentException(
                    DomainErrorMessages.DateCannotBeBeforeToday,
                    nameof(request.StartTime));
            }
            
            // Sum the duration (in minutes) for all the treatmentTypes in the query.
            var totalMinutes = treatmentTypes.Values
                .Sum(t => t.DurationMinutes);

            // Convert TimeSlot DTO til domain TimeSlot value object
            var timeSlot = new TimeSlot(
                request.StartTime, 
                request.StartTime.AddMinutes(totalMinutes)); // Automatically set endTime in case of multiple treatments(booking lines) in one booking

            if (!clinic.CanBookTimeSlot(timeSlot))
            {
                throw new BookingOutsideOpeningHoursException(clinic.Id, timeSlot);
            }

            // Find first treatment type that is a group treatment (MaxParticipants > 1)
            var groupTreatmentType = treatmentTypes
                .FirstOrDefault(kvp => kvp.Value.MaxParticipants > 1);

            // Check current number of participants for the TimeSlot against max participants allowed

            int maxParticipants;
            if (groupTreatmentType.Key != Guid.Empty)
            {
                maxParticipants = groupTreatmentType.Value.MaxParticipants;
                var currentCount = await _bookingRepository.CountParticipantsAsync(
                    groupTreatmentType.Key,
                    timeSlot);

                if (currentCount >= maxParticipants)
                {

                    return new CreateBookingResponse
                    {
                        Success = false,
                        Message = $"Der er ikke plads på holdet: ({currentCount}/{maxParticipants}). Booking Afvist."
                    };
                }
            }

            else
            {
                maxParticipants = 1;
            }

            // Calculate the customer's loyalty level based on previous bookings  
            var loyaltyLevel = _loyaltyService.GetLoyaltyLevel(completedBookings, DateTime.Now);

            //Verifying both customer and therapist against double booking

            _doubleBookingVerificationService.CustomerBookingVerification(allCustomerBooking, timeSlot);
            _doubleBookingVerificationService.TherapistVerification(allTherapistBooking, timeSlot, maxParticipants);

            // Create new Booking object using the Booking constructor
            var booking = new Booking(
                Guid.NewGuid(),
                request.CustomerId,
                request.TherapistId,
                request.ClinicId,
                timeSlot
            );
            
            if (campaignDiscount != null)
            {
                booking.ApplyCampaignDiscount(campaignDiscount.Id);
            }       

            var addOns = _priceCalculatorService.GetAutomaticAddOns(timeSlot);

            
            var allLinePrices = request.Lines
                .Select(l =>
                {
                    var treatmentType = treatmentTypes[l.TherapistTreatmentTypeId];
                    var bestPrice = _priceCalculatorService.CalculateBasePrice(treatmentType);
                    return _priceCalculatorService.ApplyAddOns(bestPrice, addOns);
                })
                .ToList();

            // Use allLinePrices to find the best price to apply discount to. Only 1 discount is applied per purchase.
            // MaxBy can compare .Value (which is decimal for Money). It's neat here, because we won't have to make Money implement IComparable<Money>. Although that isnt too difficult either..
            var mostExpensivePrice = allLinePrices.MaxBy(m => m.Value);

            var birthdayDiscountAssigned = false;

            foreach (var lineRequest in request.Lines)
            {

                var treatmentType = treatmentTypes[lineRequest.TherapistTreatmentTypeId];
                var basePrice = _priceCalculatorService.CalculateBasePrice(treatmentType);

                var priceWithAddons = _priceCalculatorService.ApplyAddOns(basePrice, addOns);

                var pricingContext = new PricingContext
                {
                    Customer = customer,
                    Booking = booking,
                    CompletedBookings = completedBookings,
                    CampaignDiscount = campaignDiscount,
                    BasePrice = priceWithAddons,
                    MostExpensiveLinePrice = mostExpensivePrice,
                    BirthdayDiscountAssigned = birthdayDiscountAssigned
                };

                var discountResult = await _priceCalculatorService
                    .CalculateBestDiscountAsync(pricingContext);

                if (discountResult.AppliedDiscount == DiscountType.Birthday)
                    birthdayDiscountAssigned = true;

                var line = new BookingLine(
                    lineRequest.TherapistTreatmentTypeId,
                    priceWithAddons,
                    discountResult.DiscountPercentage,
                    discountResult.AppliedDiscount);

                booking.AddLine(line);
            }
       

            // Create object of CreateBookingResponse DTO type to return as response
            await _bookingRepository.CreateAsync(booking);

            // Return success response
            return new CreateBookingResponse
            {
                Success = true,
                Message = "Booking oprettet!",
                Id = booking.Id,
                OriginalPrice = booking.GetBasePrice().Value,
                DiscountedPrice = booking.GetTotalPrice().Value,
                DiscountType = booking.Lines.FirstOrDefault()?.DiscountType.ToString()
            };

        }
    }
}
