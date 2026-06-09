using BookRight.Domain.Aggregates.Booking;
using BookRight.Domain.Errors;
using BookRight.Domain.Services;
using BookRight.Domain.Exceptions;
using BookRight.Domain.ValueObjects;
using BookRight.Facade.DTOs.CreateBookingDTOs;
using BookRight.Facade.Interfaces;
using BookRight.UseCases.Interfaces;
using BookRight.Domain.Enums;

namespace BookRight.UseCases.CreateBooking
{

    public class CreateBookingUseCase : ICreateBookingUseCase
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IClinicRepository _clinicRepository;
        private readonly ITreatmentTypeRepository _treatmentTypeRepository;
        private readonly ICampaignDiscountRepository _campaignDiscountRepository;

        private readonly LoyaltyService _loyaltyService;
        private readonly DoubleBookingVerificationService _doubleBookingVerificationService;
        private readonly PriceCalculatorService _priceCalculatorService;

        public CreateBookingUseCase(
            IBookingRepository bookingRepository,
            ICustomerRepository customerRepository,
            IClinicRepository clinicRepository,
            ITreatmentTypeRepository treatmentTypeRepository,
            ICampaignDiscountRepository campaignDiscountRepository,
            LoyaltyService loyaltyService,
            DoubleBookingVerificationService doubleBookingVerificationService,
            PriceCalculatorService priceCalculatorService)
        {
            // Repositories bruges til at hente og gemme data via interfaces.
            // Use casen kender derfor ikke direkte til databasen.
            _bookingRepository = bookingRepository;
            _treatmentTypeRepository = treatmentTypeRepository;
            _clinicRepository = clinicRepository;
            _customerRepository = customerRepository;
            _campaignDiscountRepository = campaignDiscountRepository;

            // Domain services indeholder forretningslogik,
            // fx loyalitet, dobbeltbooking og prisberegning.
            _loyaltyService = loyaltyService;
            _doubleBookingVerificationService = doubleBookingVerificationService;
            _priceCalculatorService = priceCalculatorService;
        }

        public async Task<CreateBookingResponse> ExecuteAsync(CreateBookingRequest request)
        {
            // Henter kunden. Hvis kunden ikke findes, stoppes use casen.
            var customer = await _customerRepository.GetByIdAsync(request.CustomerId);
            if (customer == null)
                throw new CustomerNotFoundException(request.CustomerId);

            // Henter klinikken. Booking kan ikke oprettes uden en gyldig klinik.
            var clinic = await _clinicRepository.GetByIdAsync(request.ClinicId);
            if (clinic == null)
                throw new ClinicNotFoundException(request.ClinicId);

            // Henter de behandlingstyper, der er valgt i bookingen.
            // De bruges både til varighed, pris og kombinationsregler.
            var treatmentTypes = await _treatmentTypeRepository
                .GetByTherapistTreatmentTypeIdsAsync(
                    request.Lines.Select(l => l.TherapistTreatmentTypeId)
                );

            // Hvis der vælges flere behandlinger i samme booking,
            // må ingen af dem være markeret som "kan ikke kombineres".
            if (request.Lines.Count() > 1)
            {
                var nonCombinableType = treatmentTypes.Values
                    .FirstOrDefault(t => !t.CanBeCombined);

                if (nonCombinableType != null)
                {
                    throw new ArgumentException(
                        DomainErrorMessages
                            .TreatmentTypeCannotBeCombinedWith(nonCombinableType.Name));
                }
            }

            // Henter kundens tidligere bookinger.
            // De bruges til loyalitetsberegning og rabatregler.
            var completedBookings =
                await _bookingRepository.GetAllBookingsByCustomerIdAsync(request.CustomerId);

            // Henter kundens og behandlerens eksisterende bookinger.
            // De bruges til at kontrollere dobbeltbooking.
            var allCustomerBooking =
                await _bookingRepository.GetByCustomerIdAsync(request.CustomerId);

            var allTherapistBooking =
                await _bookingRepository.GetByTherapistIdAsync(request.TherapistId);

            // Henter kampagnerabat, hvis receptionisten har valgt en.
            var campaignDiscount = request.CampaignDiscountId.HasValue
                ? await _campaignDiscountRepository.GetByIdAsync(request.CampaignDiscountId.Value)
                : null;


            // Booking må ikke oprettes i fortiden.
            // 1. Først: Er bookingdatoen overhovedet gyldig?
            if (request.StartTime < DateTime.Now)
            {
                throw new ArgumentException(
                    DomainErrorMessages.DateCannotBeBeforeToday);
            }

            // Kampagnerabat må kun bruges, hvis bookingdatoen ligger i kampagnens periode.
            // 2. Derefter: Kan den valgte kampagne bruges på den gyldige dato?
            if (campaignDiscount is not null)
            {
                var bookingDate = DateOnly.FromDateTime(request.StartTime);

                if (!campaignDiscount.IsActive(bookingDate))
                {
                    throw new CampaignDiscountNotValidException(
                        campaignDiscount.Name,
                        campaignDiscount.DateRange.StartDate);
                }
            }

            // Beregner samlet varighed.
            // Hvis der er flere behandlinger, lægges deres varigheder sammen.
            var totalMinutes = treatmentTypes.Values
                .Sum(t => t.DurationMinutes);

            // Opretter TimeSlot value object.
            // Sluttidspunktet beregnes automatisk ud fra samlet behandlingstid.
            var timeSlot = new TimeSlot(
                request.StartTime,
                request.StartTime.AddMinutes(totalMinutes));

            // Kontrollerer om bookingen ligger indenfor klinikkens åbningstid.
            if (!clinic.CanBookTimeSlot(timeSlot))
            {
                throw new BookingOutsideOpeningHoursException(clinic.Id, timeSlot);
            }

            // Finder ud af, om bookingen er en holdtræning eller anden gruppebehandling.
            var groupTreatmentType = treatmentTypes
                .FirstOrDefault(kvp => kvp.Value.MaxParticipants > 1);

            int maxParticipants;

            // Hvis behandlingen er en gruppebehandling,
            // skal systemet kontrollere om der stadig er ledige pladser.
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
                // Almindelige behandlinger har kun én deltager.
                maxParticipants = 1;
            }

            // Beregner kundens loyalitetsniveau ud fra tidligere bookinger.
            var loyaltyLevel = _loyaltyService.GetLoyaltyLevel(completedBookings, DateTime.Now);

            // Kontrollerer at kunden og behandleren ikke allerede har booking i samme tidsrum.
            _doubleBookingVerificationService.CustomerBookingVerification(allCustomerBooking, timeSlot);
            _doubleBookingVerificationService.TherapistVerification(allTherapistBooking, timeSlot, maxParticipants);

            // Opretter selve Booking aggregate.
            var booking = new Booking(
                Guid.NewGuid(),
                request.CustomerId,
                request.TherapistId,
                request.ClinicId,
                timeSlot
            );

            // Gemmer kampagnens id på bookingen, hvis kampagne er valgt.
            if (campaignDiscount != null)
            {
                booking.ApplyCampaignDiscount(campaignDiscount.Id);
            }

            // Finder automatiske tillæg, fx aften- eller weekendtillæg.
            var addOns = _priceCalculatorService.GetAutomaticAddOns(timeSlot);

            // Beregner pris med tillæg for hver bookinglinje.
            // Dette bruges til at finde den dyreste behandling,
            // fordi nogle rabatter kun må anvendes på én behandling.
            var allLinePrices = request.Lines
                .Select(l =>
                {
                    var treatmentType = treatmentTypes[l.TherapistTreatmentTypeId];
                    var bestPrice = _priceCalculatorService.CalculateBasePrice(treatmentType);
                    return _priceCalculatorService.ApplyAddOns(bestPrice, addOns);
                })
                .ToList();

            var mostExpensivePrice = allLinePrices.MaxBy(m => m.Value);

            // Bruges til at sikre, at fødselsdagsrabat kun gives én gang i bookingen.
            var birthdayDiscountAssigned = false;

            // Opretter en BookingLine for hver valgt behandling.
            foreach (var lineRequest in request.Lines)
            {
                var treatmentType = treatmentTypes[lineRequest.TherapistTreatmentTypeId];

                // Beregner grundpris og eventuelle automatiske tillæg.
                var basePrice = _priceCalculatorService.CalculateBasePrice(treatmentType);
                var priceWithAddons = _priceCalculatorService.ApplyAddOns(basePrice, addOns);

                // Samler alle oplysninger, som prisberegneren skal bruge
                // for at finde den bedste rabat.
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

                // Beregner bedste rabat.
                // Systemet vælger den rabat, der giver kunden lavest pris.
                var discountResult = await _priceCalculatorService
                    .CalculateBestDiscountAsync(pricingContext);

                // Hvis fødselsdagsrabat allerede er brugt,
                // må den ikke bruges igen på næste bookinglinje.
                if (discountResult.AppliedDiscount == DiscountType.Birthday)
                    birthdayDiscountAssigned = true;

                // Opretter bookinglinje med behandling, pris og anvendt rabattype.
                var line = new BookingLine(
                    lineRequest.TherapistTreatmentTypeId,
                    priceWithAddons,
                    discountResult.DiscountPercentage,
                    discountResult.AppliedDiscount);

                booking.AddLine(line);
            }

            // Gemmer bookingen i databasen via repository.
            await _bookingRepository.CreateAsync(booking);

            // Returnerer resultatet til UI/facade.
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