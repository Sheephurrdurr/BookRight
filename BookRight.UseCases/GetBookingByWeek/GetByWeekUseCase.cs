using BookRight.Domain.Aggregates.Booking;
using BookRight.Facade.DTOs.GetBookingsByWeekDTOs;
using BookRight.Facade.Interfaces.BookingUseCases;
using BookRight.UseCases.Interfaces;

namespace BookRight.UseCases.GetBookingByWeek
{
    public class GetByWeekUseCase : IGetByWeekUseCase
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly ITherapistRepository _therapistRepository;
        private readonly IClinicRepository _clinicRepository;
        private readonly ITreatmentTypeRepository _treatmentTypeRepository;
        private readonly ICustomerRepository _customerRepository;

        public GetByWeekUseCase(IBookingRepository bookingRepository, ITherapistRepository therapistRepository, IClinicRepository clinicRepository, ITreatmentTypeRepository treatmentTypeRepository, ICustomerRepository customerRepository)
            {
                _bookingRepository = bookingRepository;
                _therapistRepository = therapistRepository;
                _clinicRepository = clinicRepository;
                _treatmentTypeRepository = treatmentTypeRepository;
                _customerRepository = customerRepository;

            }

        public async Task<IReadOnlyList<GetByWeekResponse>> ExecuteAsync(DateOnly weekStart)
        {
            var bookings = await _bookingRepository.GetByWeekAsync(weekStart);
            var therapistTreatmentTypeIds = bookings
            .SelectMany(b => b.Lines)
            .Select(l => l.TherapistTreatmentTypeId)
            .Distinct()
            .ToList();

            var treatmentTypesByLineId =
                await _treatmentTypeRepository.GetByTherapistTreatmentTypeIdsAsync(therapistTreatmentTypeIds);
            var responses = new List<GetByWeekResponse>();

            foreach (var b in bookings)
            {
                var therapist = await _therapistRepository.GetByIdAsync(b.TherapistId);
                var clinic = await _clinicRepository.GetByIdAsync(b.ClinicId);
                var customer = await _customerRepository.GetByIdAsync(b.CustomerId);
                var treatmentName = b.Lines
                .Select(line => treatmentTypesByLineId.TryGetValue(line.TherapistTreatmentTypeId, out var treatment)
                    ? treatment.Name
                    : "")
                .FirstOrDefault() ?? "";
                responses.Add(new GetByWeekResponse(
                    b.Id,
                    b.CustomerId,
                    b.TherapistId,
                    b.TimeSlot.StartTime,
                    b.TimeSlot.EndTime,
                    b.Status.ToString(),
                    therapist?.Name.ToString() ?? "",
                    treatmentName,
                    clinic?.Name ?? "",
                    customer?.Name.ToString() ?? "",
                    customer?.Phone.ToString() ?? "",
                    customer?.Email.ToString() ?? ""
                ));
            }

            return responses;

        }
    }
}
