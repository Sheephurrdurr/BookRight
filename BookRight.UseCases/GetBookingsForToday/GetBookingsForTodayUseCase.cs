using BookRight.Facade.DTOs.GetBookingsForTodayDTOs;
using BookRight.Facade.Interfaces.BookingUseCases;
using BookRight.UseCases.Interfaces;

namespace BookRight.UseCases.GetBookingsForToday
{
    public class GetBookingsForTodayUseCase : IGetBookingsForTodayUseCase
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly ITherapistRepository _therapistRepository;

        public GetBookingsForTodayUseCase(
            IBookingRepository bookingRepository,
            ICustomerRepository customerRepository,
            ITherapistRepository therapistRepository)
        {
            _bookingRepository = bookingRepository;
            _customerRepository = customerRepository;
            _therapistRepository = therapistRepository;
        }

        public async Task<IReadOnlyList<GetBookingsForTodayResponse>> ExecuteAsync()
        {
            var bookings = await _bookingRepository.GetAllAsync();
            var customers = await _customerRepository.GetAllAsync();
            var therapists = await _therapistRepository.GetAllAsync();

            return bookings
                .Where(b => b.TimeSlot.StartTime.Date == DateTime.Today)
                .OrderBy(b => b.TimeSlot.StartTime)
                .Select(b => new GetBookingsForTodayResponse(
                    b.Id,
                    b.TimeSlot.StartTime,
                    b.ClinicId,
                    customers.FirstOrDefault(c => c.Id == b.CustomerId)?.Name.ToString() ?? "Ukendt kunde",
                    therapists.FirstOrDefault(t => t.Id == b.TherapistId)?.Name.ToString() ?? "Ukendt behandler",
                    b.Status.ToString()
                    ))
                .ToList();
        }
    }
}
