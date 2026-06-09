using BookRight.Domain.Aggregates.Booking;
using BookRight.Domain.Aggregates.Clinic;
using BookRight.Domain.Aggregates.Customer;
using BookRight.Domain.Aggregates.TherapistAggregate;
using BookRight.Domain.Aggregates.TreatmentType;
using BookRight.Domain.Enums;
using BookRight.Domain.Exceptions;
using BookRight.Domain.Services;
using BookRight.Domain.Services.DiscountStrategies;
using BookRight.Domain.ValueObjects;
using BookRight.Facade.DTOs.CreateBookingDTOs;
using BookRight.UseCases.CreateBooking;
using BookRight.UseCases.Interfaces;
using Moq;

namespace BookRight.UseCase.Test
{
    public class CreateBookingUseCaseTests
    {
        private readonly Mock<IBookingRepository> _bookingRepository = new();
        private readonly Mock<ICustomerRepository> _customerRepository = new();
        private readonly Mock<IClinicRepository> _clinicRepository = new();
        private readonly Mock<ITreatmentTypeRepository> _treatmentTypeRepository = new();
        private readonly Mock<ICampaignDiscountRepository> _campaignDiscountRepository = new();

        private readonly CreateBookingUseCase _sut;

        public CreateBookingUseCaseTests()
        {
            var priceCalculatorService =
                new PriceCalculatorService(new List<IDiscountStrategy>
                {
                    new NoDiscountStrategy()
                });

            _sut = new CreateBookingUseCase(
                _bookingRepository.Object,
                _customerRepository.Object,
                _clinicRepository.Object,
                _treatmentTypeRepository.Object,
                _campaignDiscountRepository.Object,
                new LoyaltyService(),
                new DoubleBookingVerificationService(),
                priceCalculatorService
            );
        }

        private Customer CreateCustomer()
        {
            return new Customer(
                new FullName("Test", "Customer"),
                new Email("test@test.dk"),
                new Address("TestVej", "TestBy", "0000"),
                new PhoneNumber("12345667"),
                new DateOnly(2000, 1, 1),
                healthNotes: null,
                preferredTherapistId: null
            );
        }

        private Therapist CreateTherapist(Guid clinicId, Guid therapistId)
        {
            var therapist = new Therapist(
                new FullName("Test", "Therapist"),
                new Email("therapist@test.dk"),
                "Fysioterapi",
                new Authorization("Fysioterapeut", "AUT123"),
                clinicId
            );

            SetPrivateId(therapist, therapistId);

            return therapist;
        }

        private void SetPrivateId(Therapist therapist, Guid therapistId)
        {
            typeof(Therapist)
                .GetProperty(nameof(Therapist.Id))!
                .SetValue(therapist, therapistId);
        }

        private Clinic CreateClinic(Guid therapistId)
        {
            var clinic = new Clinic(
                "Test Clinic",
                new Address("ClinicVej", "ClinicBy", "1111"),
                new PhoneNumber("7654321"),
                numTreatmentRooms: 3
            );

            clinic.AddOpeningHour(DayOfWeek.Monday, new TimeOnly(8, 0), new TimeOnly(16, 0));
            clinic.AddOpeningHour(DayOfWeek.Tuesday, new TimeOnly(8, 0), new TimeOnly(16, 0));
            clinic.AddOpeningHour(DayOfWeek.Wednesday, new TimeOnly(8, 0), new TimeOnly(16, 0));
            clinic.AddOpeningHour(DayOfWeek.Thursday, new TimeOnly(8, 0), new TimeOnly(16, 0));
            clinic.AddOpeningHour(DayOfWeek.Friday, new TimeOnly(8, 0), new TimeOnly(16, 0));

            var therapist = CreateTherapist(clinic.Id, therapistId);
            clinic.AddTherapist(therapist);

            return clinic;
        }

        private TreatmentType CreateGroupTreatmentType(int maxParticipants)
        {
            return new TreatmentType(
                "Gruppeyoga",
                durationMinutes: 60,
                maxParticipants: maxParticipants,
                new Money(200m),
                false,
                null
            );
        }

        private TreatmentType CreateSingleTreatmentType()
        {
            return new TreatmentType(
                "Massage",
                durationMinutes: 60,
                maxParticipants: 1,
                new Money(200m),
                true,
                null
            );
        }

        private void SetupValidRepositories(
            Guid customerId,
            Guid clinicId,
            Guid therapistId,
            Guid therapistTreatmentTypeId,
            TreatmentType treatmentType)
        {
            _customerRepository
                .Setup(x => x.GetByIdAsync(customerId))
                .ReturnsAsync(CreateCustomer());

            _clinicRepository
                .Setup(x => x.GetByIdAsync(clinicId))
                .ReturnsAsync(CreateClinic(therapistId));

            _treatmentTypeRepository
                .Setup(x => x.GetByTherapistTreatmentTypeIdsAsync(It.IsAny<IEnumerable<Guid>>()))
                .ReturnsAsync(new Dictionary<Guid, TreatmentType>
                {
                    { therapistTreatmentTypeId, treatmentType }
                });

            _bookingRepository
                .Setup(x => x.GetAllBookingsByCustomerIdAsync(customerId))
                .ReturnsAsync(new List<Booking>());

            _bookingRepository
                .Setup(x => x.GetByCustomerIdAsync(customerId))
                .ReturnsAsync(new List<Booking>());

            _bookingRepository
                .Setup(x => x.GetByTherapistIdAsync(therapistId))
                .ReturnsAsync(new List<Booking>());
        }

        [Fact]
        public async Task ExecuteAsync_ValidBooking_CreatesBooking()
        {
            var customerId = Guid.NewGuid();
            var clinicId = Guid.NewGuid();
            var therapistId = Guid.NewGuid();
            var therapistTreatmentTypeId = Guid.NewGuid();

            SetupValidRepositories(
                customerId,
                clinicId,
                therapistId,
                therapistTreatmentTypeId,
                CreateSingleTreatmentType()
            );

            var request = new CreateBookingRequest
            {
                CustomerId = customerId,
                ClinicId = clinicId,
                TherapistId = therapistId,
                StartTime = new DateTime(2027, 1, 4, 11, 0, 0),
                Lines = new List<BookingLineRequest>
                {
                    new BookingLineRequest
                    {
                        TherapistTreatmentTypeId = therapistTreatmentTypeId,
                        BasePrice = 200
                    }
                }
            };

            var response = await _sut.ExecuteAsync(request);

            Assert.True(response.Success);
            Assert.Equal("Booking oprettet!", response.Message);

            _bookingRepository.Verify(
                x => x.CreateAsync(It.IsAny<Booking>()),
                Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_GroupTreatmentFull_ReturnsFailedResponse()
        {
            var customerId = Guid.NewGuid();
            var clinicId = Guid.NewGuid();
            var therapistId = Guid.NewGuid();
            var therapistTreatmentTypeId = Guid.NewGuid();

            SetupValidRepositories(
                customerId,
                clinicId,
                therapistId,
                therapistTreatmentTypeId,
                CreateGroupTreatmentType(maxParticipants: 5)
            );

            _bookingRepository
                .Setup(x => x.CountParticipantsAsync(
                    therapistTreatmentTypeId,
                    It.IsAny<TimeSlot>()))
                .ReturnsAsync(5);

            var request = new CreateBookingRequest
            {
                CustomerId = customerId,
                ClinicId = clinicId,
                TherapistId = therapistId,
                StartTime = new DateTime(2027, 1, 4, 11, 0, 0),
                Lines = new List<BookingLineRequest>
                {
                    new BookingLineRequest
                    {
                        TherapistTreatmentTypeId = therapistTreatmentTypeId,
                        BasePrice = 200
                    }
                }
            };

            var response = await _sut.ExecuteAsync(request);

            Assert.False(response.Success);

            _bookingRepository.Verify(
                x => x.CreateAsync(It.IsAny<Booking>()),
                Times.Never);
        }

        [Fact]
        public async Task ExecuteAsync_BookingOutsideOpeningHours_ThrowsException()
        {
            var customerId = Guid.NewGuid();
            var clinicId = Guid.NewGuid();
            var therapistId = Guid.NewGuid();
            var therapistTreatmentTypeId = Guid.NewGuid();

            SetupValidRepositories(
                customerId,
                clinicId,
                therapistId,
                therapistTreatmentTypeId,
                CreateSingleTreatmentType()
            );

            var request = new CreateBookingRequest
            {
                CustomerId = customerId,
                ClinicId = clinicId,
                TherapistId = therapistId,
                StartTime = new DateTime(2027, 1, 4, 1, 0, 0),
                Lines = new List<BookingLineRequest>
                {
                    new BookingLineRequest
                    {
                        TherapistTreatmentTypeId = therapistTreatmentTypeId,
                        BasePrice = 200
                    }
                }
            };

            await Assert.ThrowsAsync<BookingOutsideOpeningHoursException>(
                () => _sut.ExecuteAsync(request));

            _bookingRepository.Verify(
                x => x.CreateAsync(It.IsAny<Booking>()),
                Times.Never);
        }

        private class NoDiscountStrategy : IDiscountStrategy
        {
            public DiscountResult CalculateDiscount(PricingContext context)
            {
                return new DiscountResult(
                    context.BasePrice,
                    context.BasePrice,
                    DiscountType.None
                );
            }
        }
    }
}