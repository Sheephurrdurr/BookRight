using BookRight.Domain.Aggregates.Booking;
using BookRight.Domain.Aggregates.Clinic;
using BookRight.Domain.Aggregates.Customer;
using BookRight.Domain.Aggregates.TreatmentType;
using BookRight.Domain.Services;
using BookRight.Domain.ValueObjects;
using BookRight.Facade.DTOs.CreateBookingDTOs;
using BookRight.Facade.DTOs.ValueObjectDTOs;
using BookRight.UseCases.CreateBooking;
using BookRight.UseCases.Interfaces;
using Moq;

namespace BookRight.UseCase.Test
{
    public class CreateBookingUseCaseTests
    {
        private readonly Mock<IBookingRepository> _mockBookingRepository;
        private readonly Mock<ICustomerRepository> _mockCustomerRepository;
        private readonly Mock<IClinicRepository> _mockClinicRepository;
        private readonly Mock<ITreatmentTypeRepository> _mockTreatmentTypeRepository;
        private readonly LoyaltyService _loyaltyService;
        private readonly DoubleBookingVerificationService _doubleBookingVerificationService;
        private readonly CreateBookingUseCase _sut;

        public CreateBookingUseCaseTests()
        {
            _mockBookingRepository = new Mock<IBookingRepository>();
            _mockCustomerRepository = new Mock<ICustomerRepository>();
            _mockClinicRepository = new Mock<IClinicRepository>();
            _mockTreatmentTypeRepository = new Mock<ITreatmentTypeRepository>();
            
            _loyaltyService = new LoyaltyService();
            _doubleBookingVerificationService = new DoubleBookingVerificationService();

            _sut = new CreateBookingUseCase(
                _mockBookingRepository.Object,
                _mockCustomerRepository.Object,
                _mockClinicRepository.Object,
                _mockTreatmentTypeRepository.Object,
                _loyaltyService,
                _doubleBookingVerificationService

            );
        }

        private Customer CreateTestCustomer() => new Customer(
            new FullName("Test", "Customer"),
            new Email("test@test.dk"),
            new PhoneNumber("12345667"),
            new DateOnly(2000, 1, 1),
            healthNotes: null,
            preferredTherapistId: null
            );

        private Clinic CreateTestClinic() => new Clinic(
            "Test Klinik",
            new Address("Testvej 1", "By", "2000"),
            new PhoneNumber("12345678"),
            numTreatmentRooms: 4
            );

        private TreatmentType CreateTestGroupTreatmentType(int maxParticipants) => new TreatmentType(
        "Gruppeyoga",
        durationMinutes: 60,
        maxParticipants: maxParticipants,
        new Money(200m)
        );

        [Fact]
        public async Task ExecuteAsync_GroupTreatmentFull_ReturnsFailedResponse()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var clinicId = Guid.NewGuid();
            var therapistTreatmentTypeId = Guid.NewGuid();

            _mockCustomerRepository
                .Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(CreateTestCustomer());

            _mockClinicRepository
                .Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(CreateTestClinic());

            _mockTreatmentTypeRepository
                .Setup(r => r.GetByTherapistTreatmentTypeIdsAsync(It.IsAny<IEnumerable<Guid>>()))
                .ReturnsAsync(new Dictionary<Guid, TreatmentType>
                {
                    {therapistTreatmentTypeId, CreateTestGroupTreatmentType(maxParticipants: 5) }
                });

            _mockBookingRepository
                .Setup(r => r.CountParticipantsAsync(therapistTreatmentTypeId, It.IsAny<TimeSlot>()))
                .ReturnsAsync(5);

            var request = new CreateBookingRequest
            {
                CustomerId = customerId,
                ClinicId = clinicId,
                TimeSlot = new TimeSlotDto
                {
                    StartTime = DateTime.Now.AddDays(1),
                    EndTime = DateTime.Now.AddDays(1).AddHours(1)
                },
                Lines = new List<BookingLineRequest>
                {
                    new BookingLineRequest
                    {
                        TherapistTreatmentTypeId = therapistTreatmentTypeId,
                        BasePrice = 200,
                    }
                }
            };

            // Act
            var response = await _sut.ExecuteAsync(request);

            // Assert
            Assert.False(response.Success);
            _mockBookingRepository.Verify(r => r.CreateAsync(It.IsAny<Booking>()), Times.Never);
                
        }
    }
}
